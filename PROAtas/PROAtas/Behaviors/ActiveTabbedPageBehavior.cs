using System;
using System.Linq;
using Xamarin.Forms;

namespace PROAtas.Behaviors
{
    interface IActiveAware
    {
        bool IsActive { get; set; }
        event EventHandler IsActiveChanged;
    }

    class LazyContentPageBehavior : LoadContentOnActivateBehavior<ContentView>
    {
        protected override void SetContent(ContentView element, View contentView)
        {
            element.Content = contentView;
        }
    }

    class ActiveTabbedPageBehavior : Behavior<TabbedPage>
    {
        protected override void OnAttachedTo(TabbedPage tabbedPage)
        {
            base.OnAttachedTo(tabbedPage);
            tabbedPage.CurrentPageChanged += OnTabbedPageCurrentPageChanged;
        }

        protected override void OnDetachingFrom(TabbedPage tabbedPage)
        {
            base.OnDetachingFrom(tabbedPage);
            tabbedPage.CurrentPageChanged -= OnTabbedPageCurrentPageChanged;
        }

        private void OnTabbedPageCurrentPageChanged(object sender, EventArgs e)
        {
            var tabbedPage = (TabbedPage)sender;

            // Deactivate previously selected page
            IActiveAware prevActiveAwarePage = tabbedPage.Children.OfType<IActiveAware>()
                .FirstOrDefault(c => c.IsActive && tabbedPage.CurrentPage != c);
            if (prevActiveAwarePage != null)
            {
                prevActiveAwarePage.IsActive = false;
            }

            // Activate selected page
            if (tabbedPage.CurrentPage is IActiveAware activeAwarePage)
            {
                activeAwarePage.IsActive = true;
            }
        }
    }

    abstract class LoadContentOnActivateBehavior<TActivateAwareElement> : Behavior<TActivateAwareElement> where TActivateAwareElement : VisualElement
    {
        public DataTemplate ContentTemplate { get; set; }

        protected override void OnAttachedTo(TActivateAwareElement element)
        {
            base.OnAttachedTo(element);
            (element as IActiveAware).IsActiveChanged += OnIsActiveChanged;
        }

        protected override void OnDetachingFrom(TActivateAwareElement element)
        {
            (element as IActiveAware).IsActiveChanged -= OnIsActiveChanged;
            base.OnDetachingFrom(element);
        }

        void OnIsActiveChanged(object sender, EventArgs e)
        {
            var element = (TActivateAwareElement)sender;
            element.Behaviors.Remove(this);
            SetContent(element, (View)ContentTemplate.CreateContent());
        }

        protected abstract void SetContent(TActivateAwareElement element, View contentView);
    }
}
