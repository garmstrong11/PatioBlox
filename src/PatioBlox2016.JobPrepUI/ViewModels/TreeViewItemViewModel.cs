// See http://www.codeproject.com/Articles/26288/Simplifying-the-WPF-TreeView-by-Using-the-ViewMode

namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.Collections.ObjectModel;
  using System.Windows.Input;
  using Caliburn.Micro;

  public class TreeViewItemViewModel : PropertyChangedBase
	{
		static readonly TreeViewItemViewModel DummyChild = new TreeViewItemViewModel();
		private readonly TreeViewItemViewModel _parent;
		private readonly ObservableCollection<TreeViewItemViewModel> _children;
		private bool _isExpanded;
		private bool _isSelected;
		private Cursor _cursor;

		protected TreeViewItemViewModel(TreeViewItemViewModel parent, bool lazyLoadChildren = true)
		{
			_parent = parent;
			_children = new ObservableCollection<TreeViewItemViewModel>();

			if (lazyLoadChildren) {
				_children.Add(DummyChild);
			}
		}

		private TreeViewItemViewModel() { }

		public bool HasDummyChild
		{
			get { return _children.Count == 1 && _children[0] == DummyChild; }
		}

		public ObservableCollection<TreeViewItemViewModel> Children
		{
			get { return _children; }
		}

		public TreeViewItemViewModel Parent
		{
			get { return _parent; }
		}

		public Cursor Cursor
		{
			get { return _cursor; }
			set
			{
				if (Equals(value, _cursor)) return;
				_cursor = value;
				NotifyOfPropertyChange(() => Cursor);
			}
		}

		public bool IsExpanded
		{
			get { return _isExpanded; }
			set
			{
				if (value.Equals(_isExpanded)) return;
				_isExpanded = value;
				NotifyOfPropertyChange();

				if (_isExpanded && _parent != null) {
					_parent.IsExpanded = true;
				}

				if (!HasDummyChild) return;

				Children.Remove(DummyChild);
				LoadChildren();
			}
		}

		public virtual bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				if (value.Equals(_isSelected)) return;
				_isSelected = value;
				NotifyOfPropertyChange();
			}
		}

		protected virtual void LoadChildren() { }
	}
}