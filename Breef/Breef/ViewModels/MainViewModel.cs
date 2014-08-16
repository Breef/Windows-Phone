using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Breef.Resources;

using Parse;
using System.Collections.Generic;

namespace Breef.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Stories = new ObservableCollection<Story>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<Story> Stories { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public async void LoadData()
        {
            var query = ParseObject.GetQuery("Stories").WhereLessThanOrEqualTo("updatedAt", DateTime.Now.ToUniversalTime()).OrderBy("updatedAt").OrderByDescending("updatedAt");
            query = query.Limit(10);
            IEnumerable<ParseObject> results = await query.FindAsync();

            int i = 0;
            List<Story> storiesInput = new List<Story>();

            foreach (ParseObject story in results)
            {

                storiesInput.Add
                //storiesInput.Add(new Snippet() { SnippetID = i.ToString(), Title = title, Info = info, Category = category, ImageLink = linkToImage, StoryType = storyType, CreatedOn = createdOn, UpdatedAt = updatedAt, EndDate = endDate });
                i++;

            }
            
            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}