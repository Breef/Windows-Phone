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
            this.Items = new ObservableCollection<Snippet>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<Snippet> Items { get; private set; }

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
            var query = ParseObject.GetQuery("Stories").WhereLessThanOrEqualTo("createdAt", DateTime.Now.ToUniversalTime()).OrderBy("createdAt").OrderByDescending("createdAt");
            query = query.Limit(30);
            IEnumerable<ParseObject> results = await query.FindAsync();

            int i = 0;
            List<Snippet> stories = new List<Snippet>();

            foreach (ParseObject story in results)
            {

                //All try catches for relevant story information Uncheck Region to view
                #region
                string title, info, category, linkToImage;
                int storyType;
                DateTime createdOn, endDate, updatedAt;

                try
                {
                    title = story.Get<string>("Title");
                }
                catch
                {
                    title = "";
                }

                try
                {
                    info = story.Get<string>("info");
                }
                catch
                {
                    info = "";
                }

                try
                {
                    category = story.Get<string>("category");
                }
                catch
                {
                    category = "";
                }

                try
                {
                    linkToImage = story.Get<string>("linkToImage");
                }
                catch
                {
                    linkToImage = "";
                }

                try
                {
                    storyType = story.Get<int>("storyType");
                }
                catch
                {
                    storyType = 0;
                }

                try
                {
                    createdOn = story.Get<DateTime>("createdOn");
                }
                catch
                {
                    createdOn = DateTime.Now;
                }

                try
                {
                    updatedAt = story.Get<DateTime>("updatedAt");
                }
                catch
                {
                    updatedAt = DateTime.Now;
                }

                try
                {
                    endDate = story.Get<DateTime>("endDate");
                }
                catch
                {
                    endDate = DateTime.Now;
                }

                #endregion

                this.Items.Add(new Snippet() { SnippetID = i.ToString(), Title = title, Info = info, Category = category, ImageLink = linkToImage, StoryType = storyType, CreatedOn = createdOn, UpdatedAt = updatedAt, EndDate = endDate });
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