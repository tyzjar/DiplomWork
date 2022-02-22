/*
* MATLAB Compiler: 8.1 (R2020b)
* Date: Tue Feb 15 18:21:22 2022
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

namespace gui_previewNative
{

  /// <summary>
  /// The MatlabPreview class provides a CLS compliant, Object (native) interface to the
  /// MATLAB functions contained in the files:
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
    /// Provides a void output, 0-input Objectinterface to the gui_originalView MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    ///
    public void gui_originalView()
    {
      mcr.EvaluateFunction(0, "gui_originalView", new Object[]{});
    }


    /// <summary>
    /// Provides a void output, 1-input Objectinterface to the gui_originalView MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    ///
    public void gui_originalView(Object sample)
    {
      mcr.EvaluateFunction(0, "gui_originalView", sample);
    }


    /// <summary>
    /// Provides a void output, 2-input Objectinterface to the gui_originalView MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    ///
    public void gui_originalView(Object sample, Object koef)
    {
      mcr.EvaluateFunction(0, "gui_originalView", sample, koef);
    }


    /// <summary>
    /// Provides the standard 0-input Object interface to the gui_originalView MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_originalView(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_originalView", new Object[]{});
    }


    /// <summary>
    /// Provides the standard 1-input Object interface to the gui_originalView MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_originalView(int numArgsOut, Object sample)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_originalView", sample);
    }


    /// <summary>
    /// Provides the standard 2-input Object interface to the gui_originalView MATLAB
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
    public Object[] gui_originalView(int numArgsOut, Object sample, Object koef)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_originalView", sample, koef);
    }


    /// <summary>
    /// Provides an interface for the gui_originalView function in which the input and
    /// output
    /// arguments are specified as an array of Objects.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of Object output arguments</param>
    /// <param name= "argsIn">Array of Object input arguments</param>
    /// <param name= "varArgsIn">Array of Object representing variable input
    /// arguments</param>
    ///
    [MATLABSignature("gui_originalView", 2, 0, 0)]
    protected void gui_originalView(int numArgsOut, ref Object[] argsOut, Object[] argsIn, params Object[] varArgsIn)
    {
        mcr.EvaluateFunctionForTypeSafeCall("gui_originalView", numArgsOut, ref argsOut, argsIn, varArgsIn);
    }
    /// <summary>
    /// Provides a void output, 0-input Objectinterface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    ///
    public void gui_preview()
    {
      mcr.EvaluateFunction(0, "gui_preview", new Object[]{});
    }


    /// <summary>
    /// Provides a void output, 1-input Objectinterface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    ///
    public void gui_preview(Object sample)
    {
      mcr.EvaluateFunction(0, "gui_preview", sample);
    }


    /// <summary>
    /// Provides a void output, 2-input Objectinterface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    ///
    public void gui_preview(Object sample, Object koef)
    {
      mcr.EvaluateFunction(0, "gui_preview", sample, koef);
    }


    /// <summary>
    /// Provides a void output, 3-input Objectinterface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="Lowpass">Input argument #3</param>
    ///
    public void gui_preview(Object sample, Object koef, Object Lowpass)
    {
      mcr.EvaluateFunction(0, "gui_preview", sample, koef, Lowpass);
    }


    /// <summary>
    /// Provides a void output, 4-input Objectinterface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="Lowpass">Input argument #3</param>
    /// <param name="Hipass">Input argument #4</param>
    ///
    public void gui_preview(Object sample, Object koef, Object Lowpass, Object Hipass)
    {
      mcr.EvaluateFunction(0, "gui_preview", sample, koef, Lowpass, Hipass);
    }


    /// <summary>
    /// Provides a void output, 5-input Objectinterface to the gui_preview MATLAB
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
    public void gui_preview(Object sample, Object koef, Object Lowpass, Object Hipass, 
                      Object THRESHOLD)
    {
      mcr.EvaluateFunction(0, "gui_preview", sample, koef, Lowpass, Hipass, THRESHOLD);
    }


    /// <summary>
    /// Provides the standard 0-input Object interface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_preview(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_preview", new Object[]{});
    }


    /// <summary>
    /// Provides the standard 1-input Object interface to the gui_preview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_preview(int numArgsOut, Object sample)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_preview", sample);
    }


    /// <summary>
    /// Provides the standard 2-input Object interface to the gui_preview MATLAB
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
    public Object[] gui_preview(int numArgsOut, Object sample, Object koef)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_preview", sample, koef);
    }


    /// <summary>
    /// Provides the standard 3-input Object interface to the gui_preview MATLAB
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
    public Object[] gui_preview(int numArgsOut, Object sample, Object koef, Object 
                          Lowpass)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_preview", sample, koef, Lowpass);
    }


    /// <summary>
    /// Provides the standard 4-input Object interface to the gui_preview MATLAB
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
    public Object[] gui_preview(int numArgsOut, Object sample, Object koef, Object 
                          Lowpass, Object Hipass)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_preview", sample, koef, Lowpass, Hipass);
    }


    /// <summary>
    /// Provides the standard 5-input Object interface to the gui_preview MATLAB
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
    public Object[] gui_preview(int numArgsOut, Object sample, Object koef, Object 
                          Lowpass, Object Hipass, Object THRESHOLD)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_preview", sample, koef, Lowpass, Hipass, THRESHOLD);
    }


    /// <summary>
    /// Provides an interface for the gui_preview function in which the input and output
    /// arguments are specified as an array of Objects.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of Object output arguments</param>
    /// <param name= "argsIn">Array of Object input arguments</param>
    /// <param name= "varArgsIn">Array of Object representing variable input
    /// arguments</param>
    ///
    [MATLABSignature("gui_preview", 5, 0, 0)]
    protected void gui_preview(int numArgsOut, ref Object[] argsOut, Object[] argsIn, params Object[] varArgsIn)
    {
        mcr.EvaluateFunctionForTypeSafeCall("gui_preview", numArgsOut, ref argsOut, argsIn, varArgsIn);
    }
    /// <summary>
    /// Provides a void output, 0-input Objectinterface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    ///
    public void gui_sfilterPreview()
    {
      mcr.EvaluateFunction(0, "gui_sfilterPreview", new Object[]{});
    }


    /// <summary>
    /// Provides a void output, 1-input Objectinterface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    ///
    public void gui_sfilterPreview(Object sample)
    {
      mcr.EvaluateFunction(0, "gui_sfilterPreview", sample);
    }


    /// <summary>
    /// Provides a void output, 2-input Objectinterface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    ///
    public void gui_sfilterPreview(Object sample, Object koef)
    {
      mcr.EvaluateFunction(0, "gui_sfilterPreview", sample, koef);
    }


    /// <summary>
    /// Provides a void output, 3-input Objectinterface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="Lowpass">Input argument #3</param>
    ///
    public void gui_sfilterPreview(Object sample, Object koef, Object Lowpass)
    {
      mcr.EvaluateFunction(0, "gui_sfilterPreview", sample, koef, Lowpass);
    }


    /// <summary>
    /// Provides a void output, 4-input Objectinterface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="Lowpass">Input argument #3</param>
    /// <param name="Hipass">Input argument #4</param>
    ///
    public void gui_sfilterPreview(Object sample, Object koef, Object Lowpass, Object 
                             Hipass)
    {
      mcr.EvaluateFunction(0, "gui_sfilterPreview", sample, koef, Lowpass, Hipass);
    }


    /// <summary>
    /// Provides the standard 0-input Object interface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_sfilterPreview(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_sfilterPreview", new Object[]{});
    }


    /// <summary>
    /// Provides the standard 1-input Object interface to the gui_sfilterPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_sfilterPreview(int numArgsOut, Object sample)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_sfilterPreview", sample);
    }


    /// <summary>
    /// Provides the standard 2-input Object interface to the gui_sfilterPreview MATLAB
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
    public Object[] gui_sfilterPreview(int numArgsOut, Object sample, Object koef)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_sfilterPreview", sample, koef);
    }


    /// <summary>
    /// Provides the standard 3-input Object interface to the gui_sfilterPreview MATLAB
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
    public Object[] gui_sfilterPreview(int numArgsOut, Object sample, Object koef, Object 
                                 Lowpass)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_sfilterPreview", sample, koef, Lowpass);
    }


    /// <summary>
    /// Provides the standard 4-input Object interface to the gui_sfilterPreview MATLAB
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
    public Object[] gui_sfilterPreview(int numArgsOut, Object sample, Object koef, Object 
                                 Lowpass, Object Hipass)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_sfilterPreview", sample, koef, Lowpass, Hipass);
    }


    /// <summary>
    /// Provides an interface for the gui_sfilterPreview function in which the input and
    /// output
    /// arguments are specified as an array of Objects.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of Object output arguments</param>
    /// <param name= "argsIn">Array of Object input arguments</param>
    /// <param name= "varArgsIn">Array of Object representing variable input
    /// arguments</param>
    ///
    [MATLABSignature("gui_sfilterPreview", 4, 0, 0)]
    protected void gui_sfilterPreview(int numArgsOut, ref Object[] argsOut, Object[] argsIn, params Object[] varArgsIn)
    {
        mcr.EvaluateFunctionForTypeSafeCall("gui_sfilterPreview", numArgsOut, ref argsOut, argsIn, varArgsIn);
    }
    /// <summary>
    /// Provides a void output, 0-input Objectinterface to the gui_thresholdPreview
    /// MATLAB function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    ///
    public void gui_thresholdPreview()
    {
      mcr.EvaluateFunction(0, "gui_thresholdPreview", new Object[]{});
    }


    /// <summary>
    /// Provides a void output, 1-input Objectinterface to the gui_thresholdPreview
    /// MATLAB function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    ///
    public void gui_thresholdPreview(Object sample)
    {
      mcr.EvaluateFunction(0, "gui_thresholdPreview", sample);
    }


    /// <summary>
    /// Provides a void output, 2-input Objectinterface to the gui_thresholdPreview
    /// MATLAB function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    ///
    public void gui_thresholdPreview(Object sample, Object koef)
    {
      mcr.EvaluateFunction(0, "gui_thresholdPreview", sample, koef);
    }


    /// <summary>
    /// Provides a void output, 3-input Objectinterface to the gui_thresholdPreview
    /// MATLAB function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="sample">Input argument #1</param>
    /// <param name="koef">Input argument #2</param>
    /// <param name="THRESHOLD">Input argument #3</param>
    ///
    public void gui_thresholdPreview(Object sample, Object koef, Object THRESHOLD)
    {
      mcr.EvaluateFunction(0, "gui_thresholdPreview", sample, koef, THRESHOLD);
    }


    /// <summary>
    /// Provides the standard 0-input Object interface to the gui_thresholdPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_thresholdPreview(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_thresholdPreview", new Object[]{});
    }


    /// <summary>
    /// Provides the standard 1-input Object interface to the gui_thresholdPreview MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="sample">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_thresholdPreview(int numArgsOut, Object sample)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_thresholdPreview", sample);
    }


    /// <summary>
    /// Provides the standard 2-input Object interface to the gui_thresholdPreview MATLAB
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
    public Object[] gui_thresholdPreview(int numArgsOut, Object sample, Object koef)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_thresholdPreview", sample, koef);
    }


    /// <summary>
    /// Provides the standard 3-input Object interface to the gui_thresholdPreview MATLAB
    /// function.
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
    public Object[] gui_thresholdPreview(int numArgsOut, Object sample, Object koef, 
                                   Object THRESHOLD)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_thresholdPreview", sample, koef, THRESHOLD);
    }


    /// <summary>
    /// Provides an interface for the gui_thresholdPreview function in which the input
    /// and output
    /// arguments are specified as an array of Objects.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of Object output arguments</param>
    /// <param name= "argsIn">Array of Object input arguments</param>
    /// <param name= "varArgsIn">Array of Object representing variable input
    /// arguments</param>
    ///
    [MATLABSignature("gui_thresholdPreview", 3, 0, 0)]
    protected void gui_thresholdPreview(int numArgsOut, ref Object[] argsOut, Object[] argsIn, params Object[] varArgsIn)
    {
        mcr.EvaluateFunctionForTypeSafeCall("gui_thresholdPreview", numArgsOut, ref argsOut, argsIn, varArgsIn);
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
