package learning.app1;

import learning.app1.util.SystemUiHider;
import learning.app1.preview.Preview;

import android.annotation.TargetApi;
import android.app.Activity;
import android.content.Context;
import android.content.pm.PackageManager;
import android.hardware.Camera;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;
import android.widget.FrameLayout;
import android.widget.ProgressBar;
import android.widget.TextView;

import java.util.Timer;
import java.util.TimerTask;


/**
 * An example full-screen activity that shows and hides the system UI (i.e.
 * status bar and navigation/system bar) with user interaction.
 *
 * @see SystemUiHider
 */
public class FullscreenActivity extends Activity {

    /************************* ACTIVITY STATES *************************/
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_fullscreen);

        final View controlsView = findViewById(R.id.fullscreen_content_controls);
        final View contentView = findViewById(R.id.fullscreen_contents);

        // Set up an instance of SystemUiHider to control the system UI for
        // this activity.
        mSystemUiHider = SystemUiHider.getInstance(this, contentView, HIDER_FLAGS);
        mSystemUiHider.setup();
        mSystemUiHider
                .setOnVisibilityChangeListener(new SystemUiHider.OnVisibilityChangeListener() {
                    // Cached values.
                    int mControlsHeight;
                    int mShortAnimTime;

                    @Override
                    @TargetApi(Build.VERSION_CODES.HONEYCOMB_MR2)
                    public void onVisibilityChange(boolean visible) {
                        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.HONEYCOMB_MR2) {
                            // If the ViewPropertyAnimator API is available
                            // (Honeycomb MR2 and later), use it to animate the
                            // in-layout UI controls at the bottom of the
                            // screen.
                            if (mControlsHeight == 0) {
                                mControlsHeight = controlsView.getHeight();
                            }
                            if (mShortAnimTime == 0) {
                                mShortAnimTime = getResources().getInteger(
                                        android.R.integer.config_shortAnimTime);
                            }
                            controlsView.animate()
                                    .translationY(visible ? 0 : mControlsHeight)
                                    .setDuration(mShortAnimTime);
                        } else {
                            // If the ViewPropertyAnimator APIs aren't
                            // available, simply show or hide the in-layout UI
                            // controls.
                            controlsView.setVisibility(visible ? View.VISIBLE : View.GONE);
                        }

                        if (visible && AUTO_HIDE) {
                            // Schedule a hide().
                            delayedHide(AUTO_HIDE_DELAY_MILLIS);
                        }
                    }
                });

        // Set up the user interaction to manually show or hide the system UI.
        contentView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if (TOGGLE_ON_CLICK) {
                    mSystemUiHider.toggle();
                } else {
                    mSystemUiHider.show();
                }
            }
        });

        // Upon interacting with UI controls, delay any scheduled hide()
        // operations to prevent the jarring behavior of controls going away
        // while interacting with the UI.

        findViewById(R.id.help_button).setOnTouchListener(mDelayHideTouchListener);
        findViewById(R.id.settings_button).setOnTouchListener(mDelayHideTouchListener);
        findViewById(R.id.exit_button).setOnTouchListener(mDelayHideTouchListener);

        mTimer = new Timer();
        mTimer.schedule(new synthesizerTask(), 0, 500);
    }

    @Override
    protected void onResume() {
        super.onResume();
        setupCamera();
    }

    @Override
    protected void onPause() {
       // releaseCamera();
        super.onPause();
    }

    @Override
    public void onDestroy(){
        super.onDestroy();
        //m_synthesizer.stop(); TODO
    }

    @Override
    protected void onPostCreate(Bundle savedInstanceState) {
        super.onPostCreate(savedInstanceState);

        // Trigger the initial hide() shortly after the activity has been
        // created, to briefly hint to the user that UI controls
        // are available.
        delayedHide(100);
    }

    /************************* TIMER *************************/
    private Timer mTimer;

    // Update the synthesizer settings based on camera image
    class synthesizerTask extends TimerTask {
        @Override
        public void run() {
            h.sendEmptyMessage(0);
        }
    };

    //this  posts a message to the main thread from our timertask
    final Handler h = new Handler(new Handler.Callback() {
        @Override
        public boolean handleMessage(Message msg) {
            //m_synthesizer.setFrequency(mPreview.getRed(), mPreview.getGreen(), mPreview.getBlue());

            // Get the colors intensity
            int red = (int) (mPreview.getRed() * 100);
            int green = (int) (mPreview.getGreen() * 100);
            int blue = (int) (mPreview.getBlue() * 100);

            // Update visuals or color intensity
            ((ProgressBar) findViewById(R.id.red_percentage)).setProgress(red);
            ((ProgressBar) findViewById(R.id.green_percentage)).setProgress(green);
            ((ProgressBar) findViewById(R.id.blue_percentage)).setProgress(blue);

            // Update the text for color intensity
            ((TextView) findViewById(R.id.red_text)).setText("Red: " + red + "%");
            ((TextView) findViewById(R.id.green_text)).setText("Green: " + green + "%");
            ((TextView) findViewById(R.id.blue_text)).setText("Blue: " + blue + "%");

            // Guess the final color based on the intensities
            String deducedColor = deduceColor(red, green, blue);
            ((TextView) findViewById(R.id.color_view)).setText("Color: " + deducedColor);

            return false;
        }

        // If any color is present by at least this many percent in the sum, it's considered PRESENT
        // Example, RGB values are 30, 40, 70, summary for 140,
        //  PARAM = 27%
        //  Red is 30/140 -> 21%        -----
        //  Green is 40/140 -> 28%      PRESENT!
        //  Blue is 70/140 -> 50%       PRESENT!
        //          -> Green and Blue = Teal (seledynowy)
        private static final double COLOR_PERCENTAGE_TO_CONSIDER_PRESENT = 0.01 *
                27.0;

        // Based on levels of RED, GREEN and BLUE, try to guess the real color, return the string for it
        private String deduceColor(int red, int green, int blue) {
            // Initial variables
            double total = red + green + blue;

            // Check if colors are present
            boolean isRed, isGreen, isBlue;
            isRed = (red / total) >= COLOR_PERCENTAGE_TO_CONSIDER_PRESENT;
            isGreen = (green / total) >= COLOR_PERCENTAGE_TO_CONSIDER_PRESENT;
            isBlue = (blue / total) >= COLOR_PERCENTAGE_TO_CONSIDER_PRESENT;

            // Guess the color!
            if(isRed)
            {
                if(isGreen)
                {
                    if(isBlue)
                    {
                        if(total >= 150)
                            return "White";         // RED, GREEN, BLUE, intense
                        else
                            return "Black;";        // RED, GREEN, BLUE, weak
                    }
                    else
                    {
                        return "Yellow";            // RED, GREEN
                    }
                }
                else
                {
                    if(isBlue)
                    {
                        return "Purple";            // RED, BLUE
                    }
                    else
                    {
                        return "Red";               // RED
                    }
                }
            }
            else
            {
                if(isGreen)
                {
                    if(isBlue)
                    {
                        return "Teal";              // GREEN, BLUE
                    }
                    else
                    {
                        return "Green";            // GREEN
                    }
                }
                else
                {
                    if(isBlue)
                    {
                        return "Blue";            // BLUE
                    }
                    else
                    {
                        return "NO COLOR";
                    }
                }
            }
        }
    });

    /************************* UI HIDER *************************/
    /**
     * Whether or not the system UI should be auto-hidden after
     * {@link #AUTO_HIDE_DELAY_MILLIS} milliseconds.
     */
    private static final boolean AUTO_HIDE = true;

    /**
     * If {@link #AUTO_HIDE} is set, the number of milliseconds to wait after
     * user interaction before hiding the system UI.
     */
    private static final int AUTO_HIDE_DELAY_MILLIS = 3000;

    /**
     * If set, will toggle the system UI visibility upon interaction. Otherwise,
     * will show the system UI visibility upon interaction.
     */
    private static final boolean TOGGLE_ON_CLICK = true;

    /**
     * The flags to pass to {@link SystemUiHider#getInstance}.
     */
    private static final int HIDER_FLAGS = SystemUiHider.FLAG_HIDE_NAVIGATION;

    /**
     * The instance of the {@link SystemUiHider} for this activity.
     */
    private SystemUiHider mSystemUiHider;

    /**
     * Touch listener to use for in-layout UI controls to delay hiding the
     * system UI. This is to prevent the jarring behavior of controls going away
     * while interacting with activity UI.
     */
    View.OnTouchListener mDelayHideTouchListener = new View.OnTouchListener() {
        @Override
        public boolean onTouch(View view, MotionEvent motionEvent) {
            if (AUTO_HIDE) {
                delayedHide(AUTO_HIDE_DELAY_MILLIS);
            }
            return false;
        }
    };

    Handler mHideHandler = new Handler();
    Runnable mHideRunnable = new Runnable() {
        @Override
        public void run() {
            mSystemUiHider.hide();
        }
    };

    /**
     * Schedules a call to hide() in [delay] milliseconds, canceling any
     * previously scheduled calls.
     */
    private void delayedHide(int delayMillis) {
        mHideHandler.removeCallbacks(mHideRunnable);
        mHideHandler.postDelayed(mHideRunnable, delayMillis);
    }


    /************************* CAMERA *************************/
    private Camera mCamera;
   // private SurfaceView mContentView;
    private Preview mPreview;

    private void setupCamera() {
        if (checkCameraHardware(this)) {
            mCamera = getCameraInstance();
            mPreview = (Preview) findViewById(R.id.preview_view);
            mPreview.setCamera(mCamera);


     //       mContentView.removeAllViews();
       //     mContentView.addView(mPreview);
        }
    }

    private void releaseCamera() {
        try {
            mCamera.stopPreview();
            mCamera.setPreviewCallback(null);
            mCamera.release();
            mCamera = null;
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private boolean checkCameraHardware(Context context) {
        if (context.getPackageManager().hasSystemFeature(PackageManager.FEATURE_CAMERA)) {
            return true;
        } else {
            return false;
        }
    }

    public static Camera getCameraInstance() {
        Camera c = null;
        try {
            c = Camera.open();
            Log.e("LEARNING-APP1", "Camera OK!");
        } catch (Exception e) {
            Log.e("LEARNING-APP1", "Camera is not available");
        }
        return c;
    }

}
