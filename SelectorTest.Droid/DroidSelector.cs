using System;
using System.Collections.Generic;
using System.Threading;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Print;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using SelectorTest.Core;
using Object = Java.Lang.Object;

namespace SelectorTest.Droid
{
    [Activity(Label = "Droid Selector", Theme = "@style/AppTheme")]
    public class DroidSelector : AppCompatActivity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.view_droid_selector);
            var rv = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            rv.SetLayoutManager(new LinearLayoutManager(this));
            rv.SetAdapter(new DroidRecyclerViewAdapter(GetItems()));
        }

        private List<DroidItem> GetItems()
        {
            return new List<DroidItem>
            {
                DroidItem.One,
                DroidItem.Two,
                DroidItem.Three,
                DroidItem.One,
                DroidItem.Two,
                DroidItem.Three,
                DroidItem.One,
                DroidItem.Two,
                DroidItem.Three,
                DroidItem.One,
                DroidItem.Two,
                DroidItem.Three,
                DroidItem.One,
                DroidItem.Two,
                DroidItem.Three,
                DroidItem.One,
                DroidItem.Two,
                DroidItem.Three,
                DroidItem.One,
                DroidItem.Two,
                DroidItem.Three
            };
        }
    }

    public class DroidRecyclerViewAdapter : RecyclerView.Adapter
    {
        private readonly List<DroidItem> _items;
        private readonly ILogger _logger = new Logger();

        public DroidRecyclerViewAdapter(List<DroidItem> items)
        {
            _items = items;
        }

        public override int ItemCount => _items.Count;

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var inflater = LayoutInflater.From(parent.Context);
            var view = inflater.Inflate(Resource.Layout.template_droid_item, parent, false);
            var viewHolder = new DroidViewHolder(view);

            return viewHolder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
#if NoBinding

            if (position >= 0 && position < _items.Count && holder is DroidViewHolder droidViewHolder)
            {
                switch (_items[position])
                {
                    case DroidItem.One:
                        droidViewHolder.OneIsChecked = true;
                        break;
                    case DroidItem.Two:
                        droidViewHolder.TwoIsChecked = true;
                        break;
                    case DroidItem.Three:
                        droidViewHolder.ThreeIsChecked = true;
                        break;
                }
            }
#else
            _logger.Debug($"Start: Pos: {position}");
            if (position >= 0 && position < _items.Count && holder is DroidViewHolder droidViewHolder)
            {
                _logger.Debug($"before bind. Pos: {position}");
                droidViewHolder.Pos = position;
                droidViewHolder.DataContext = _items[position];
                _logger.Debug($"after bind. Pos: {position}");
            }
            _logger.Debug($"Finish: Pos: {position}");
#endif
        }

#if !NoBinding

        public override void OnViewAttachedToWindow(Object holder)
        {
            var droidHolder = holder as DroidViewHolder;
            var pos = droidHolder?.Pos ?? -1;

            _logger.Debug($"Start. Pos {pos}");
            base.OnViewAttachedToWindow(holder);
            _logger.Debug($"after base call. Pos {pos}");
            droidHolder?.OnViewAttachedToWindow();
            _logger.Debug($"Finished. Pos {pos}");
        }

        public override void OnViewDetachedFromWindow(Object holder)
        {
            var droidHolder = holder as DroidViewHolder;
            var pos = droidHolder?.Pos ?? -1;
            _logger.Debug($"Start. Pos {pos}");
            base.OnViewDetachedFromWindow(holder);
            _logger.Debug($"after base. Pos {pos}");
            droidHolder?.OnViewDetachedFromWindow();
            _logger.Debug($"Finish. Pos {pos}");
        }

        public override void OnViewRecycled(Object holder)
        {
            base.OnViewRecycled(holder);

            if (!(holder is DroidViewHolder droidHolder)) return;
            droidHolder.OnViewRecycled();
        }
#endif

    }


    // stores and recycles views as they are scrolled off screen
    public class DroidViewHolder : RecyclerView.ViewHolder
    {
        private RadioButton _one;
        private RadioButton _two;
        private RadioButton _three;
        private DroidItem? _cachedDataContext;
        private DroidItem? _dataContext;
        private Guid _id = Guid.NewGuid(); 
        private readonly ILogger _logger = new Logger();

        public DroidViewHolder(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public DroidViewHolder(View itemView) : base(itemView)
        {
            _one = itemView.FindViewById<RadioButton>(Resource.Id.one);
            _two = itemView.FindViewById<RadioButton>(Resource.Id.two);
            _three = itemView.FindViewById<RadioButton>(Resource.Id.three);
            Pos = -2;
        }

        public bool RequestMainThreadAction(Action action, bool maskExceptions = true)
        {
            Application.SynchronizationContext.Post(ignored =>
            {
                action();
            }, null);

            return true;
        }


        public DroidItem? DataContext
        {
            get => _dataContext;
            set
            {
                _dataContext = value;

                _logger.Debug($"Setting widgets {_dataContext.ToString()}. Pos {Pos}, Id = {_id.ToString()}");

                //if (DataContext != null)
                //{
                //    OneIsChecked = DataContext == DroidItem.One;
                //    TwoIsChecked = DataContext == DroidItem.Two;
                //    ThreeIsChecked = DataContext == DroidItem.Three;
                //}

                if (DataContext == null)
                {
                    var test = "";
                }

                OneIsChecked = DataContext == DroidItem.One;
                TwoIsChecked = DataContext == DroidItem.Two;
                ThreeIsChecked = DataContext == DroidItem.Three;

                if (value != null)
                {
                    _logger.Debug($"Setting Cached to null. Pos {Pos}, Id = {_id.ToString()}");
                    _cachedDataContext = null;
                }
            }
        }

        public int Pos { get; set; }

        public bool OneIsChecked
        {
            get => _one.Checked;
            set => _one.Checked = value;
        }

        public bool TwoIsChecked
        {
            get => _two.Checked;
            set => _two.Checked = value;
        }

        public bool ThreeIsChecked
        {
            get => _three.Checked;
            set => _three.Checked = value;
        }

        public void OnViewAttachedToWindow()
        {
            _logger.Debug($"Start. Pos {Pos}, Id = {_id.ToString()}");
            var cached = (_cachedDataContext == null) ? "null" : _cachedDataContext.ToString();
            _logger.Debug($"cached data Context = {cached}. Pos {Pos}, Id = {_id.ToString()}");

            var data = (DataContext == null) ? "null" : DataContext.ToString();
            _logger.Debug($"data Context = {data}. Pos {Pos}, Id = {_id.ToString()}");

            if (_cachedDataContext != null && DataContext == null)
            {
                _logger.Debug($"Set DataContext with cache. Pos {Pos}, Id = {_id.ToString()}");
                DataContext = _cachedDataContext;
            }
            _logger.Debug($"Finished. Pos {Pos}, Id = {_id.ToString()}");
        }

        public void OnViewDetachedFromWindow()
        {
            _logger.Debug($"Start. Pos {Pos}, Id = {_id.ToString()}");

            _cachedDataContext = DataContext;
            DataContext = null;

            var cached = (_cachedDataContext == null) ? "null" : _cachedDataContext.ToString();
            _logger.Debug($"cached data Context = {cached}. Pos {Pos}, Id = {_id.ToString()}");

            var data = (DataContext == null) ? "null" : DataContext.ToString();
            _logger.Debug($"data Context = {data}. Pos {Pos}, Id = {_id.ToString()}");

            _logger.Debug($"Finished. Pos {Pos}, Id = {_id.ToString()}");
        }

        public virtual void OnViewRecycled()
        {
            _logger.Debug($"Start. Pos {Pos}, Id = {_id.ToString()}");
            _cachedDataContext = null;
            DataContext = null;
            _logger.Debug($"Finished. Pos {Pos}, Id = {_id.ToString()}");
        }
    }
}