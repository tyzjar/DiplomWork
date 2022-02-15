/*
* MATLAB Compiler: 8.1 (R2020b)
* Date: Mon Feb 14 19:04:36 2022
* Arguments:
* "-B""macro_default""-W""dotnet:gui_preview,MatlabPreview,4.0,private,version=1.0""-T""li
* nk:lib""-d""P:\DiplomRabota\GUI\MatlabReferences\gui_preview\for_testing""-v""class{Matl
* abPreview:P:\DiplomRabota\GUI\MatlabReferences\gui_originalView.m,P:\DiplomRabota\GUI\Ma
* tlabReferences\gui_preview.m,P:\DiplomRabota\GUI\MatlabReferences\gui_sfilterPreview.m,P
* :\DiplomRabota\GUI\MatlabReferences\gui_thresholdPreview.m}"
*/
using System;
using System.Reflection;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

#if SHARED
[assembly: System.Reflection.AssemblyKeyFile(@"")]
#endif

namespace gui_preview
{

  /// <summary>
  /// The MatlabPreview class provides a CLS compliant, MWArray interface to the MATLAB
  /// functions contained in the files:
  /// <newpara></newpara>
  /// P:\DiplomRabota\GUI\MatlabReferences\gui_originalView.m
  /// <newpara></newpara>
  /// P:\DiplomRabota\GUI\MatlabReferences\gui_preview.m
  /// <newpara></newpara>
  /// P:\DiplomRabota\GUI\MatlabReferences\gui_sfilterPreview.m
  /// <newpara></newpara>
  /// P:\DiplomRabota\GUI\MatlabReferences\gui_thresholdPreview.m
  /// </summary>
  /// <remarks>
  /// @Version 1.0
  /// </remarks>
  public class MatlabPreview : IDisposable
  {
    #region Constructors

    /// <summary internal= "true">
    /// The static constructor instantiates and initializes the MATLAB Runtime instance.
    /// </summary>
    static MatlabPreview()
    {
      if (MWMCR.MCRAppInitialized)
      {
        try
        {
          Assembly assembly= Assembly.GetExecutingAssembly();

          string ctfFilePath= assembly.Location;

		  int lastDelimiter = ctfFilePath.LastIndexOf(@"/");

	      if (lastDelimiter == -1)
		  {
		    lastDelimiter = ctfFilePath.LastIndexOf(@"\");
		  }

          ctfFilePath= ctfFilePath.Remove(lastDelimiter, (ctfFilePath.Length - lastDelimiter));

          string ctfFileName = "gui_preview.ctf";

          Stream embeddedCtfStream = null;

          String[] resourceStrings = assembly.GetManifestResourceNames();

          foreach (String name in resourceStrings)
          {
            if (name.Contains(ctfFileName))
            {
              embeddedCtfStream = assembly.GetManifestResourceStream(name);
              break;
            }
          }
          mcr= new MWMCR("",
                         ctfFilePath, embeddedCtfStream, true);
        }
        catch(Exception ex)
        {
          ex_ = new Exception("MWArray assembly failed to be initialized", ex);
        }
      }
      else
      {
        ex_ = new ApplicationException("MWArray assembly could not be initialized");
      }
    }


    /// <summary>
    /// Constructs a new instance of the MatlabPreview class.
    /// </summary>
    public MatlabPreview()
    {
      if(ex_ != null)
      {
        throw ex_;
      }
    }


    #endregion Constructors

    #region Finalize

    /// <summary internal= "true">
    /// Class destructor called by the CLR garbage collector.
    /// </summary>
    ~MatlabPreview()
    {
      Dispose(false);
    }


    /// <summary>
    /// Frees the native resources associated with this object
    /// </summary>
    public void Dispose()
    {
      Dispose(true);

      GC.SuppressFinalize(this);
    }


    /// <summary internal= "true">
    /// Internal dispose function
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposed)
      {
        disposed= true;

        if (disposing)
        {
          // Free managed resources;
        }

        // Free native resources
      }
    }


    #endregion Finalize

    #region Methods

    /// <summary>
    /// Provides a void output, 0-input MWArrayinterface to the gui_originalView MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    ///
    public void gui_originalView()
    {
      mcr.EvaluateFunction(0, "gui_originalView", new MWArray[]{});
    }


    /// <summary>
    /// Provides a void output, 1-input MWArrayinterface to the gui_originalView MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    ///
    public void gui_originalView(MWArray sample)
    {
      mcr.EvaluateFunction(0, "gui_originalView", sample);
    }


    /// <summary>
    /// Provides a void output, 2-input MWArrayinterface to the gui_originalView MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    ///
    public void gui_originalView(MWArray sample, MWArray koef)
    {
      mcr.EvaluateFunction(0, "gui_originalView", sample, koef);
    }


    /// <summary>
    /// Provides the standard 0-input MWArray interface to the gui_originalView MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_originalView(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_originalView", new MWArray[]{});
    }


    /// <summary>
    /// Provides the standard 1-input MWArray interface to the gui_originalView MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_originalView(int numArgsOut, MWArray sample)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_originalView", sample);
    }


    /// <summary>
    /// Provides the standard 2-input MWArray interface to the gui_originalView MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_originalView(int numArgsOut, MWArray sample, MWArray koef)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_originalView", sample, koef);
    }


    /// <summary>
    /// Provides a void output, 0-input MWArrayinterface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    ///
    public void gui_preview()
    {
      mcr.EvaluateFunction(0, "gui_preview", new MWArray[]{});
    }


    /// <summary>
    /// Provides a void output, 1-input MWArrayinterface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    ///
    public void gui_preview(MWArray sample)
    {
      mcr.EvaluateFunction(0, "gui_preview", sample);
    }


    /// <summary>
    /// Provides a void output, 2-input MWArrayinterface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    ///
    public void gui_preview(MWArray sample, MWArray koef)
    {
      mcr.EvaluateFunction(0, "gui_preview", sample, koef);
    }


    /// <summary>
    /// Provides a void output, 3-input MWArrayinterface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="Lowpass">Input argument #3</param>
    ///
    public void gui_preview(MWArray sample, MWArray koef, MWArray Lowpass)
    {
      mcr.EvaluateFunction(0, "gui_preview", sample, koef, Lowpass);
    }


    /// <summary>
    /// Provides a void output, 4-input MWArrayinterface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="Lowpass">Input argument #3</param>
    /// <param name="Hipass">Input argument #4</param>
    ///
    public void gui_preview(MWArray sample, MWArray koef, MWArray Lowpass, MWArray Hipass)
    {
      mcr.EvaluateFunction(0, "gui_preview", sample, koef, Lowpass, Hipass);
    }


    /// <summary>
    /// Provides a void output, 5-input MWArrayinterface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="Lowpass">Input argument #3</param>
    /// <param name="Hipass">Input argument #4</param>
    /// <param name="THRESHOLD">Input argument #5</param>
    ///
    public void gui_preview(MWArray sample, MWArray koef, MWArray Lowpass, MWArray 
                      Hipass, MWArray THRESHOLD)
    {
      mcr.EvaluateFunction(0, "gui_preview", sample, koef, Lowpass, Hipass, THRESHOLD);
    }


    /// <summary>
    /// Provides the standard 0-input MWArray interface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_preview(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_preview", new MWArray[]{});
    }


    /// <summary>
    /// Provides the standard 1-input MWArray interface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_preview(int numArgsOut, MWArray sample)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_preview", sample);
    }


    /// <summary>
    /// Provides the standard 2-input MWArray interface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_preview(int numArgsOut, MWArray sample, MWArray koef)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_preview", sample, koef);
    }


    /// <summary>
    /// Provides the standard 3-input MWArray interface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="Lowpass">Input argument #3</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_preview(int numArgsOut, MWArray sample, MWArray koef, MWArray 
                           Lowpass)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_preview", sample, koef, Lowpass);
    }


    /// <summary>
    /// Provides the standard 4-input MWArray interface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="Lowpass">Input argument #3</param>
    /// <param name="Hipass">Input argument #4</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_preview(int numArgsOut, MWArray sample, MWArray koef, MWArray 
                           Lowpass, MWArray Hipass)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_preview", sample, koef, Lowpass, Hipass);
    }


    /// <summary>
    /// Provides the standard 5-input MWArray interface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="Lowpass">Input argument #3</param>
    /// <param name="Hipass">Input argument #4</param>
    /// <param name="THRESHOLD">Input argument #5</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_preview(int numArgsOut, MWArray sample, MWArray koef, MWArray 
                           Lowpass, MWArray Hipass, MWArray THRESHOLD)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_preview", sample, koef, Lowpass, Hipass, THRESHOLD);
    }


    /// <summary>
    /// Provides a void output, 0-input MWArrayinterface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    ///
    public void gui_sfilterPreview()
    {
      mcr.EvaluateFunction(0, "gui_sfilterPreview", new MWArray[]{});
    }


    /// <summary>
    /// Provides a void output, 1-input MWArrayinterface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    ///
    public void gui_sfilterPreview(MWArray sample)
    {
      mcr.EvaluateFunction(0, "gui_sfilterPreview", sample);
    }


    /// <summary>
    /// Provides a void output, 2-input MWArrayinterface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    ///
    public void gui_sfilterPreview(MWArray sample, MWArray koef)
    {
      mcr.EvaluateFunction(0, "gui_sfilterPreview", sample, koef);
    }


    /// <summary>
    /// Provides a void output, 3-input MWArrayinterface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="Lowpass">Input argument #3</param>
    ///
    public void gui_sfilterPreview(MWArray sample, MWArray koef, MWArray Lowpass)
    {
      mcr.EvaluateFunction(0, "gui_sfilterPreview", sample, koef, Lowpass);
    }


    /// <summary>
    /// Provides a void output, 4-input MWArrayinterface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="Lowpass">Input argument #3</param>
    /// <param name="Hipass">Input argument #4</param>
    ///
    public void gui_sfilterPreview(MWArray sample, MWArray koef, MWArray Lowpass, MWArray 
                             Hipass)
    {
      mcr.EvaluateFunction(0, "gui_sfilterPreview", sample, koef, Lowpass, Hipass);
    }


    /// <summary>
    /// Provides the standard 0-input MWArray interface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_sfilterPreview(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_sfilterPreview", new MWArray[]{});
    }


    /// <summary>
    /// Provides the standard 1-input MWArray interface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_sfilterPreview(int numArgsOut, MWArray sample)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_sfilterPreview", sample);
    }


    /// <summary>
    /// Provides the standard 2-input MWArray interface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_sfilterPreview(int numArgsOut, MWArray sample, MWArray koef)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_sfilterPreview", sample, koef);
    }


    /// <summary>
    /// Provides the standard 3-input MWArray interface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="Lowpass">Input argument #3</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_sfilterPreview(int numArgsOut, MWArray sample, MWArray koef, 
                                  MWArray Lowpass)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_sfilterPreview", sample, koef, Lowpass);
    }


    /// <summary>
    /// Provides the standard 4-input MWArray interface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="Lowpass">Input argument #3</param>
    /// <param name="Hipass">Input argument #4</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_sfilterPreview(int numArgsOut, MWArray sample, MWArray koef, 
                                  MWArray Lowpass, MWArray Hipass)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_sfilterPreview", sample, koef, Lowpass, Hipass);
    }


    /// <summary>
    /// Provides a void output, 0-input MWArrayinterface to the gui_thresholdPreview
    /// MATLAB function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    ///
    public void gui_thresholdPreview()
    {
      mcr.EvaluateFunction(0, "gui_thresholdPreview", new MWArray[]{});
    }


    /// <summary>
    /// Provides a void output, 1-input MWArrayinterface to the gui_thresholdPreview
    /// MATLAB function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    ///
    public void gui_thresholdPreview(MWArray sample)
    {
      mcr.EvaluateFunction(0, "gui_thresholdPreview", sample);
    }


    /// <summary>
    /// Provides a void output, 2-input MWArrayinterface to the gui_thresholdPreview
    /// MATLAB function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    ///
    public void gui_thresholdPreview(MWArray sample, MWArray koef)
    {
      mcr.EvaluateFunction(0, "gui_thresholdPreview", sample, koef);
    }


    /// <summary>
    /// Provides a void output, 3-input MWArrayinterface to the gui_thresholdPreview
    /// MATLAB function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="THRESHOLD">Input argument #3</param>
    ///
    public void gui_thresholdPreview(MWArray sample, MWArray koef, MWArray THRESHOLD)
    {
      mcr.EvaluateFunction(0, "gui_thresholdPreview", sample, koef, THRESHOLD);
    }


    /// <summary>
    /// Provides the standard 0-input MWArray interface to the gui_thresholdPreview
    /// MATLAB function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_thresholdPreview(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_thresholdPreview", new MWArray[]{});
    }


    /// <summary>
    /// Provides the standard 1-input MWArray interface to the gui_thresholdPreview
    /// MATLAB function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_thresholdPreview(int numArgsOut, MWArray sample)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_thresholdPreview", sample);
    }


    /// <summary>
    /// Provides the standard 2-input MWArray interface to the gui_thresholdPreview
    /// MATLAB function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_thresholdPreview(int numArgsOut, MWArray sample, MWArray koef)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_thresholdPreview", sample, koef);
    }


    /// <summary>
    /// Provides the standard 3-input MWArray interface to the gui_thresholdPreview
    /// MATLAB function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="THRESHOLD">Input argument #3</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_thresholdPreview(int numArgsOut, MWArray sample, MWArray koef, 
                                    MWArray THRESHOLD)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_thresholdPreview", sample, koef, THRESHOLD);
    }



    /// <summary>
    /// This method will cause a MATLAB figure window to behave as a modal dialog box.
    /// The method will not return until all the figure windows associated with this
    /// component have been closed.
    /// </summary>
    /// <remarks>
    /// An application should only call this method when required to keep the
    /// MATLAB figure window from disappearing.  Other techniques, such as calling
    /// Console.ReadLine() from the application should be considered where
    /// possible.</remarks>
    ///
    public void WaitForFiguresToDie()
    {
      mcr.WaitForFiguresToDie();
    }



    #endregion Methods

    #region Class Members

    private static MWMCR mcr= null;

    private static Exception ex_= null;

    private bool disposed= false;

    #endregion Class Members
  }
}
