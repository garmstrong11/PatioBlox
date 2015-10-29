namespace PatioBlox2016.JobPrepUI.Infra
{
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Interactivity;

  public class FocusBehavior : Behavior<Control>
  {
    public bool HasInitialFocus
    {
      get { return (bool)GetValue(HasInitialFocusProperty); }
      set { SetValue(HasInitialFocusProperty, value); }
    }

    public bool IsFocused
    {
      get { return (bool)GetValue(IsFocusedProperty); }
      set { SetValue(IsFocusedProperty, value); }
    }

    // Using a DependencyProperty as the backing store for HasInitialFocus.  
    // This enables animation, styling, binding, etc...
    public static readonly DependencyProperty HasInitialFocusProperty =
        DependencyProperty.Register(
          "HasInitialFocus", 
          typeof(bool), 
          typeof(FocusBehavior), 
          new PropertyMetadata(false, null));

    // Using a DependencyProperty as the backing store for IsFocused.  
    // This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsFocusedProperty =
        DependencyProperty.Register(
          "IsFocused", 
          typeof(bool), 
          typeof(FocusBehavior), 
          new PropertyMetadata(false, 
            (d,e) => { if ((bool) e.NewValue) ((FocusBehavior) d).AssociatedObject.Focus(); }));

    protected override void OnAttached()
    {
      AssociatedObject.GotFocus += (sender, args) => IsFocused = true;
      AssociatedObject.LostFocus += (sender, args) => IsFocused = false;
      AssociatedObject.Loaded += (o, a) => { if (HasInitialFocus || IsFocused) AssociatedObject.Focus(); };
    }
  }
}