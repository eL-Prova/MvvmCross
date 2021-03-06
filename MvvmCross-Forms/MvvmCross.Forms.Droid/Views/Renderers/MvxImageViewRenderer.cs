﻿using System.ComponentModel;
using Android.Widget;
using Mvvmcross.Forms.Droid.Views;
using Mvvmcross.Forms.Views;
using MvvmCross.Platform.Platform;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;
using MvxDroidImageView = MvvmCross.Binding.Droid.Views.MvxImageView;

[assembly: ExportRenderer(typeof(MvxImageView), typeof(MvxImageViewRenderer))]
namespace Mvvmcross.Forms.Droid.Views
{
    class MvxImageViewRenderer : ImageRenderer
    {
        private MvxDroidImageView _nativeControl;
        private MvxImageView SharedControl => Element as MvxImageView;

        protected override ImageView CreateNativeControl()
        {
            _nativeControl = new MvxDroidImageView(Context, OnSourceImageChanged);

            return _nativeControl;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> args)
        {
            if (args.OldElement != null)
            {
                if (_nativeControl != null)
                {
                    _nativeControl.Dispose();
                    _nativeControl = null;
                }
            }
            
            if (Element.Source != null)
            {
                MvxTrace.Warning("Source property ignored on MvxImageView");
            }

            base.OnElementChanged(args);

            if (_nativeControl != null)
            {
                if (_nativeControl.ErrorImagePath != SharedControl.ErrorImagePath)
                {
                    _nativeControl.ErrorImagePath = SharedControl.ErrorImagePath;
                }

                if (_nativeControl.DefaultImagePath != SharedControl.DefaultImagePath)
                {
                    _nativeControl.DefaultImagePath = SharedControl.DefaultImagePath;
                }

                if (_nativeControl.ImageUrl != SharedControl.ImageUri)
                {
                    _nativeControl.ImageUrl = SharedControl.ImageUri;
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(MvxImageView.Source))
            {
                MvxTrace.Warning("Source property ignored on MvxImageView");
            }
            else
            {
                base.OnElementPropertyChanged(sender, args);

                if (_nativeControl != null)
                {
                    if (args.PropertyName == nameof(MvxImageView.DefaultImagePath))
                    {
                        _nativeControl.DefaultImagePath = SharedControl.DefaultImagePath;
                    }
                    else if (args.PropertyName == nameof(MvxImageView.ErrorImagePath))
                    {
                        _nativeControl.ErrorImagePath = SharedControl.ErrorImagePath;
                    }
                    else if (args.PropertyName == nameof(MvxImageView.ImageUri))
                    {
                        _nativeControl.ImageUrl = SharedControl.ImageUri;
                    }
                }
            }
        }

        private void OnSourceImageChanged()
        {
            (SharedControl as IVisualElementController).InvalidateMeasure(InvalidationTrigger.MeasureChanged);
        }
    }
}
