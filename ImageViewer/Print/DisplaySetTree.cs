
#region License

// Copyright (c) 2013, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This file is part of the ClearCanvas RIS/PACS open source project.
//
// The ClearCanvas RIS/PACS open source project is free software: you can
// redistribute it and/or modify it under the terms of the GNU General Public
// License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// The ClearCanvas RIS/PACS open source project is distributed in the hope that it
// will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General
// Public License for more details.
//
// You should have received a copy of the GNU General Public License along with
// the ClearCanvas RIS/PACS open source project.  If not, see
// <http://www.gnu.org/licenses/>.

#endregion

using System;
using Macro.Common.Utilities;
using Macro.Desktop;
using Macro.Desktop.Trees;

namespace Macro.ImageViewer
{
    public class DisplaySetTree : IDisposable
    {
        private readonly string _primaryStudyInstanceUid;
        private Tree<DisplaySetTreeGroupItem> _tree;
        private ISelection _selection;
        private ObservableList<IImageSet> _imageSets;
        private DisplaySetTreeItemBingding _bingding;

        public DisplaySetTree(ObservableList<IImageSet> imageSets)
            : this(imageSets, new DisplaySetTreeItemBingding())
        {

        }

        /// <summary>
        /// Constructor that allows a custom binding to be supplied.
        /// </summary>
        public DisplaySetTree(ObservableList<IImageSet> imageSets, DisplaySetTreeItemBingding binding)
        {
            _bingding = binding;
            _tree = new Tree<DisplaySetTreeGroupItem>(binding);
            _imageSets = new ObservableList<IImageSet>();
            Initialize(imageSets);
        }

        private void Initialize(ObservableList<IImageSet> imageSets)
        {
            if (imageSets.Count == 0)
            {
                return;
            }

            foreach (IImageSet imageSet in imageSets)
            {

                _tree.Items.Add(new DisplaySetTreeGroupItem(imageSet, _bingding));
                if (!_imageSets.Contains(imageSet))
                    _imageSets.Add(imageSet);
            }

        }

        public ObservableList<IImageSet> ImageSets
        {
            get { return _imageSets; }
        }

        public void AddTreeItem(ObservableList<IImageSet> imageSets)
        {
            if (_tree == null)
            {
                return;
            }

            Initialize(imageSets);
        }

        public ISelection Selection
        {
            get
            {
                //we need the actual variable to be able to remain null, so we know when to update it automatically
                return _selection ?? new Selection();
            }
            set
            {
                value = value ?? new Selection();

                if (!Object.Equals(value, _selection))
                {
                    _selection = value;
                    OnSelectionChanged();
                }
            }
        }

        public event EventHandler SelectionChanged;


        public ITree TreeRoot
        {
            get { return _tree; }
        }

        #region Private Methods


        private void OnSelectionChanged()
        {
            EventsHelper.Fire(SelectionChanged, this, EventArgs.Empty);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _tree = null;
            _imageSets.Clear();
            _imageSets = null;
            _bingding = null;
        }

        #endregion
    }

    public interface IDisplaySetTreeItem
    {
        DisplaySetTreeGroupItem Parent { get; }

        string Name { get; }
        string Number { get; }
        string Description { get; }
        bool IsExpanded { get; set; }
    }

    public class DisplaySetTreeGroupItem : IDisplaySetTreeItem, IDisposable
    {
        private Tree<IDisplaySetTreeItem> _tree;
        private IImageSet _imageSet;
        private DisplaySetTreeGroupItem _parent;

        public DisplaySetTreeGroupItem(DisplaySetTreeGroupItem parent, IImageSet imageSet, ITreeItemBinding binding)
            : this(imageSet, binding)
        {
            _parent = parent;
        }

        public DisplaySetTreeGroupItem(IImageSet imageSet, ITreeItemBinding binding)
        {
            _imageSet = imageSet;
            _tree = new Tree<IDisplaySetTreeItem>(binding);
            Initialize();
            IsExpanded = false;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _parent = null;
        }

        #endregion

        #region IDisplaySetTreeItem 成员

        public DisplaySetTreeGroupItem Parent
        {
            get { return _parent; }
        }

        public string Name
        {
            get { return _imageSet.Name; }
        }

        public string Number
        {
            get { return "1"; }
        }

        public string Description
        {
            get { return _imageSet.Descriptor.PatientInfo; }
        }

        public bool IsExpanded { get; set; }

        #endregion

        public ITree Tree
        {
            get { return _tree; }
        }

        private void Initialize()
        {
            foreach (IDisplaySet displaySet in _imageSet.DisplaySets)
            {
                _tree.Items.Add(new DisplaySetTreeItem(displaySet, this));
            }
        }

        public override string ToString()
        {
            return this.Description;
        }

    }

    public class DisplaySetTreeItem : IDisplaySetTreeItem
    {

        private IDisplaySet _displaySet;
        private DisplaySetTreeGroupItem _parent;


        internal DisplaySetTreeItem(IDisplaySet displaySet, DisplaySetTreeGroupItem displaySetTreeGroupItem)
        {
            _displaySet = displaySet;
            _parent = displaySetTreeGroupItem;
        }

        #region IDisplaySetTreeItem 成员

        public string Name
        {
            get { return _displaySet.Name; }
        }

        public string Description
        {
            get { return _displaySet.Description; }
        }

        public string Number
        {
            get { return _displaySet.Number.ToString(); }
        }

        public IDisplaySet DisplaySet
        {
            get { return _displaySet; }
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2}", this.Number, this.Name, this.Description);
        }

        public DisplaySetTreeGroupItem Parent
        {
            get { return _parent; }
        }

        public bool IsExpanded { get; set; }

        #endregion

    }

    public class DisplaySetTreeItemBingding : TreeItemBindingBase
    {

        public override string GetNodeText(object item)
        {
            return ((IDisplaySetTreeItem)item).Name;
        }

        public override string GetTooltipText(object item)
        {
            return ((IDisplaySetTreeItem)item).Description;
        }

        public override bool GetExpanded(object item)
        {
            return ((IDisplaySetTreeItem)item).IsExpanded;
        }

        public override void SetExpanded(object item, bool expanded)
        {
            ((IDisplaySetTreeItem)item).IsExpanded = expanded;
        }

        public override bool CanHaveSubTree(object item)
        {
            return item is DisplaySetTreeGroupItem;
        }

        public override ITree GetSubTree(object item)
        {
            if (item is DisplaySetTreeGroupItem)
                return ((DisplaySetTreeGroupItem)item).Tree;

            return null;
        }
    }

}
