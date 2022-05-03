using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace GUI.Items.Framework.View.MyComboBox
{
   class MyComboBox : DependencyObject
   {
      public static readonly DependencyProperty IsFilterOnAutocompleteEnabledProperty =
        DependencyProperty.RegisterAttached(
          "IsFilterOnAutocompleteEnabled",
          typeof(bool),
          typeof(MyComboBox),
          new PropertyMetadata(default(bool), MyComboBox.OnIsFilterOnAutocompleteEnabledChanged));

      public static void SetIsFilterOnAutocompleteEnabled(DependencyObject attachingElement, bool value) =>
        attachingElement.SetValue(MyComboBox.IsFilterOnAutocompleteEnabledProperty, value);

      public static bool GetIsFilterOnAutocompleteEnabled(DependencyObject attachingElement) =>
        (bool)attachingElement.GetValue(MyComboBox.IsFilterOnAutocompleteEnabledProperty);
      // Use hash tables for faster lookup
      private static Dictionary<TextBox, ComboBox> TextBoxComboBoxMap { get; }
      private static Dictionary<TextBox, int> TextBoxSelectionStartMap { get; }
      private static Dictionary<ComboBox, TextBox> ComboBoxTextBoxMap { get; }
      private static bool IsNavigationKeyPressed { get; set; }

      static MyComboBox()
      {
         MyComboBox.TextBoxComboBoxMap = new Dictionary<TextBox, ComboBox>();
         MyComboBox.TextBoxSelectionStartMap = new Dictionary<TextBox, int>();
         MyComboBox.ComboBoxTextBoxMap = new Dictionary<ComboBox, TextBox>();
      }
      private static void OnIsFilterOnAutocompleteEnabledChanged(
        DependencyObject attachingElement,
        DependencyPropertyChangedEventArgs e)
      {
         if (!(attachingElement is ComboBox comboBox && comboBox.IsEditable))
         {
            return;
         }
         if (!(bool)e.NewValue)
         {
            MyComboBox.DisableAutocompleteFilter(comboBox);
            return;
         }

         if (!comboBox.IsLoaded)
         {
            comboBox.Loaded += MyComboBox.EnableAutocompleteFilterOnComboBoxLoaded;
            return;
         }
         MyComboBox.EnableAutocompleteFilter(comboBox);
      }

      private static async void FilterOnTextInput(object sender, TextChangedEventArgs e)
      {
         await Application.Current.Dispatcher.InvokeAsync(
           () =>
           {
              if (MyComboBox.IsNavigationKeyPressed)
              {
                 return;
              }

              var textBox = sender as TextBox;
              int textBoxSelectionStart = textBox.SelectionStart;
              MyComboBox.TextBoxSelectionStartMap[textBox] = textBoxSelectionStart;

              string changedTextOnAutocomplete = textBox.Text.Substring(0, textBoxSelectionStart);
              if (MyComboBox.TextBoxComboBoxMap.TryGetValue(
              textBox, out ComboBox comboBox))
              {
                 //MessageBox.Show("FilterOnTextInput");
                 comboBox.IsDropDownOpen = true;

                 comboBox.Items.Filter = item => item.ToString().StartsWith(
                 textBox.Text,
                 StringComparison.OrdinalIgnoreCase);
              }
           },
           DispatcherPriority.Background);
      }

      private static async void HandleKeyDownWhileFiltering(object sender, KeyEventArgs e)
      {
         var comboBox = sender as ComboBox;
         if (!MyComboBox.ComboBoxTextBoxMap.TryGetValue(comboBox, out TextBox textBox))
         {
            return;
         }

         switch (e.Key)
         {
            case Key.Down
              when comboBox.Items.CurrentPosition < comboBox.Items.Count - 1
                   && comboBox.Items.MoveCurrentToNext():
            case Key.Up
              when comboBox.Items.CurrentPosition > 0
                   && comboBox.Items.MoveCurrentToPrevious():
               {
                  MyComboBox.IsNavigationKeyPressed = true;
                  MessageBox.Show("HandleKeyDownWhileFiltering 2");
                  await Application.Current.Dispatcher.InvokeAsync(
                    () =>
                    {
                       MyComboBox.SelectCurrentItem(textBox, comboBox);
                       MyComboBox.IsNavigationKeyPressed = false;
                    },
                    DispatcherPriority.ContextIdle);

                  break;
               }
         }
      }
      private static void SelectCurrentItem(TextBox textBox, ComboBox comboBox)
      {
         comboBox.SelectedItem = comboBox.Items.CurrentItem;
         if (MyComboBox.TextBoxSelectionStartMap.TryGetValue(textBox, out int selectionStart))
         {
            textBox.SelectionStart = selectionStart;
         }
      }
      private static void EnableAutocompleteFilterOnComboBoxLoaded(object sender, RoutedEventArgs e)
      {
         var comboBox = sender as ComboBox;
         MyComboBox.EnableAutocompleteFilter(comboBox);
      }
      private static void EnableAutocompleteFilter(ComboBox comboBox)
      {
         if (comboBox.TryFindVisualChildElement(out TextBox editTextBox))
         {
            MyComboBox.TextBoxComboBoxMap.Add(editTextBox, comboBox);
            MyComboBox.ComboBoxTextBoxMap.Add(comboBox, editTextBox);
            editTextBox.TextChanged += MyComboBox.FilterOnTextInput;
            comboBox.AddHandler(UIElement.PreviewKeyDownEvent, new KeyEventHandler(HandleKeyDownWhileFiltering), true);
         }
      }
      private static void DisableAutocompleteFilter(ComboBox comboBox)
      {
         if (comboBox.TryFindVisualChildElement(out TextBox editTextBox))
         {
            MyComboBox.TextBoxComboBoxMap.Remove(editTextBox);
            editTextBox.TextChanged -= MyComboBox.FilterOnTextInput;
         }
      }
   }


   public static class Extensions
   {
      public static bool TryFindVisualChildElement<TChild>(this DependencyObject parent, out TChild resultElement)
        where TChild : DependencyObject
      {
         resultElement = null;

         if (parent is Popup popup)
         {
            parent = popup.Child;
            if (parent == null)
            {
               return false;
            }
         }

         for (var childIndex = 0; childIndex < VisualTreeHelper.GetChildrenCount(parent); childIndex++)
         {
            DependencyObject childElement = VisualTreeHelper.GetChild(parent, childIndex);
            if (childElement is TChild child)
            {
               resultElement = child;
               return true;
            }

            if (childElement.TryFindVisualChildElement(out resultElement))
            {
               return true;
            }
         }
         return false;
      }
   } 
}