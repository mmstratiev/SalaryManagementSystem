using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SalaryManagementSystem.Model
{
    class ListViewColumnSorter
    {
        private GridViewColumnHeader    lastHeaderSorted;
        private ListSortDirection       lastSortDirection;
        private ListView                listView;

        public ListViewColumnSorter(ListView listView)
        {
            this.listView = listView;
        }

        public void HandleColumnClick(object sender, RoutedEventArgs e)
        {
            var columnHeader = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (columnHeader != null)
            {
                if (columnHeader.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (columnHeader != lastHeaderSorted)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (lastSortDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var columnBinding = columnHeader.Column.DisplayMemberBinding as Binding;
                    var sortBy = columnBinding?.Path.Path ?? columnHeader.Column.Header as string;

                    Sort(sortBy, direction);

                    lastHeaderSorted = columnHeader;
                    lastSortDirection = direction;
                }
            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(listView.ItemsSource);
            if (dataView != null)
            {
                dataView.SortDescriptions.Clear();
                SortDescription sd = new SortDescription(sortBy, direction);
                dataView.SortDescriptions.Add(sd);
                dataView.Refresh();
            }
        }
    }
}
