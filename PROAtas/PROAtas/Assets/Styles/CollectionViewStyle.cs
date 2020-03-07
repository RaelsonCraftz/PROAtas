using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PROAtas.Assets.Styles
{
    public static class CollectionViewStyle
    {
        public static TCollectionView VerticalListStyle<TCollectionView>(this TCollectionView collection) where TCollectionView : CollectionView
        {
            collection.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
            {
                ItemSpacing = 5,
            };

            return collection;
        }

        public static TCollectionView HorizontalListStyle<TCollectionView>(this TCollectionView collection) where TCollectionView : CollectionView
        {
            collection.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal)
            {
                ItemSpacing = 5,
            };

            return collection;
        }

        public static TCollectionView Single<TCollectionView>(this TCollectionView collection) where TCollectionView : CollectionView
        {
            collection.SelectionMode = SelectionMode.Single;

            return collection;
        }

        
    }
}
