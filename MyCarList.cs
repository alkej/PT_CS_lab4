using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Media.Imaging;

namespace PT_lab4
{
    internal class MyCarList<T> : BindingList<Car>
    {
        public MyCarList(IList<Car> list) : base(list)
        {

        }
        private bool _isSorted;

        private bool _isEngineSearch = false;

        protected override bool SupportsSortingCore => true;
        protected override bool SupportsSearchingCore => true;

        protected override bool IsSortedCore
        {
            get { return _isSorted; }
        }
        protected override void RemoveSortCore()
        {
            _isSorted = false;
        }


        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            if (prop.PropertyType.GetInterface("IComparable") != null) {

                List<Car> items = this.Items as List<Car>;
                
                if (items != null)
                {
                    switch (prop.Name)
                    {
                        case "Model":
                            items.Sort((x, y) => x.Model.CompareTo(y.Model));
                            break;
                        case "Motor":
                            items.Sort((x, y) => x.Motor.CompareTo(y.Motor));
                            break;
                        case "Year":
                            items.Sort((x, y) => x.Year.CompareTo(y.Year));
                            break;

                    }

                    _isSorted = true;

                    if (direction == ListSortDirection.Descending)
                    {
                        items.Reverse();
                    }

                    this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));

                }
                else
                {
                    _isSorted = false;
                }

            }
        }

        public int Find(string columnName, string key)
        {
            string[] splitted = columnName.Split('.');
            PropertyDescriptor prop;

            if (splitted[0] == "Motor") {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Engine));
                prop = properties.Find(splitted[1], true);
                _isEngineSearch = true;
            }
            else
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(Car));
                prop = properties.Find(columnName, true);
                _isEngineSearch = false;

            }

            if (prop != null)
            {
                return FindCore(prop, key);

            }

            return -1;
        }

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            if (prop == null) return -1;

            List<Car> items = this.Items as List<Car>;

            foreach (Car item in items)
            {
                
                if (_isEngineSearch)
                {
                    // Test column search value
                    string value = prop.GetValue(item.Motor).ToString();

                    // If value is the search value, return the 
                    // index of the data item
                    if ((string)key == value) return IndexOf(item);
                }
                else
                {
                    string value = prop.GetValue(item).ToString();

                    if ((string)key == value) return IndexOf(item);
                }
                
            }
            return -1;
        }

    }
}