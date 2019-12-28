using System;
using FlutterSharp.UI;

namespace FlutterSharp.SDK.Internal
{
    public enum SchedulerPhase
    {
        /// No frame is being processed. Tasks (scheduled by
        /// [WidgetsBinding.scheduleTask]), microtasks (scheduled by
        /// [scheduleMicrotask]), [Timer] callbacks, event handlers (e.g. from user
        /// input), and other callbacks (e.g. from [Future]s, [Stream]s, and the like)
        /// may be executing.
        idle,

        /// The transient callbacks (scheduled by
        /// [WidgetsBinding.scheduleFrameCallback]) are currently executing.
        ///
        /// Typically, these callbacks handle updating objects to new animation
        /// states.
        ///
        /// See [SchedulerBinding.handleBeginFrame].
        transientCallbacks,

        /// Microtasks scheduled during the processing of transient callbacks are
        /// current executing.
        ///
        /// This may include, for instance, callbacks from futures resolved during the
        /// [transientCallbacks] phase.
        midFrameMicrotasks,

        /// The persistent callbacks (scheduled by
        /// [WidgetsBinding.addPersistentFrameCallback]) are currently executing.
        ///
        /// Typically, this is the build/layout/paint pipeline. See
        /// [WidgetsBinding.drawFrame] and [SchedulerBinding.handleDrawFrame].
        persistentCallbacks,

        /// The post-frame callbacks (scheduled by
        /// [WidgetsBinding.addPostFrameCallback]) are currently executing.
        ///
        /// Typically, these callbacks handle cleanup and scheduling of work for the
        /// next frame.
        ///
        /// See [SchedulerBinding.handleDrawFrame].
        postFrameCallbacks,
    }

    public class SchedulerBinding : BindingBase
    {
        public SchedulerBinding()
        {
        }

        /// Whether this scheduler has requested that [handleBeginFrame] be called soon.
        public bool HasScheduledFrame => _hasScheduledFrame;
        private bool _hasScheduledFrame = false;

        /// The phase that the scheduler is currently operating under.
        public SchedulerPhase SchedulerPhase => _schedulerPhase;
        private SchedulerPhase _schedulerPhase = SchedulerPhase.idle;

        // Whether frames are currently being scheduled when [scheduleFrame] is called.
        ///
        /// This value depends on the value of the [lifecycleState].
        private bool _framesEnabled = true;
        public bool FramesEnabled
        {
            get
            {
                return _framesEnabled;
            }
            set
            {
                if (_framesEnabled == value)
                    return;
                _framesEnabled = value;
                if (value)
                    ScheduleFrame();
            }
        }

        protected void EnsureFrameCallbacksRegistered()
        {
            Window.OnBeginFrame = _handleBeginFrame;
            Window.OnDrawFrame = _handleDrawFrame;
        }

        public void EnsureVisualUpdate()
        {
            switch (SchedulerPhase)
            {
                case SchedulerPhase.idle:
                case SchedulerPhase.postFrameCallbacks:
                    ScheduleFrame();
                    return;
                case SchedulerPhase.transientCallbacks:
                case SchedulerPhase.midFrameMicrotasks:
                case SchedulerPhase.persistentCallbacks:
                    return;
            }
        }

        public void ScheduleFrame()
        {
            if (_hasScheduledFrame || !_framesEnabled)
                return;

            EnsureFrameCallbacksRegistered();
            Window.ScheduleFrame();
            _hasScheduledFrame = true;
        }

        public void ScheduleForcedFrame()
        {
            if (_hasScheduledFrame)
                return;

            Window.ScheduleFrame();
            _hasScheduledFrame = true;
        }

        bool _ignoreNextEngineDrawFrame = false;
        private bool _warmUpFrame = false;
        Duration _currentFrameTimeStamp;

        private void _handleBeginFrame(Duration rawTimeStamp)
        {
            if (_warmUpFrame)
            {
                _ignoreNextEngineDrawFrame = true;
                return;
            }
            HandleBeginFrame(rawTimeStamp);
        }

        private void _handleDrawFrame()
        {
            if (_ignoreNextEngineDrawFrame)
            {
                _ignoreNextEngineDrawFrame = false;
                return;
            }
            HandleDrawFrame();
        }

        public void HandleBeginFrame(Duration rawTimeStamp)
        {
            _currentFrameTimeStamp = rawTimeStamp;


            _hasScheduledFrame = false;
            try
            {
                // TRANSIENT FRAME CALLBACKS
                _schedulerPhase = SchedulerPhase.transientCallbacks;
                TransientCallback?.Invoke(rawTimeStamp);
            }
            finally
            {
                _schedulerPhase = SchedulerPhase.midFrameMicrotasks;
            }
        }

        public event FrameCallback TransientCallback;
        public event FrameCallback PersistentCallback;
        public event FrameCallback PostFrameCallback;


        public void HandleDrawFrame()
        {
            try
            {
                // PERSISTENT FRAME CALLBACKS
                _schedulerPhase = SchedulerPhase.persistentCallbacks;
                PersistentCallback?.Invoke(_currentFrameTimeStamp);


                // POST-FRAME CALLBACKS
                _schedulerPhase = SchedulerPhase.postFrameCallbacks;
                PostFrameCallback?.Invoke(_currentFrameTimeStamp);

            }
            finally
            {
                _schedulerPhase = SchedulerPhase.idle;
                _currentFrameTimeStamp = null;
            }
        }
    }
}
