/*
* MATLAB Compiler: 8.1 (R2020b)
* Date: Thu Jun  2 19:25:11 2022
* Arguments:
* "-B""macro_default""-W""dotnet:gui_cellcount,CellCountMatlab,4.0,private,version=1.0""-T
* ""link:lib""-d""P:\DiplomRabota\GUI\MatlabReferences\gui_cellcount\for_testing""-v""clas
* s{CellCountMatlab:P:\DiplomRabota\GUI\MatlabReferences\gui_cellcount.m}"
*/
using System;
using System.Reflection;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

#if SHARED
[assembly: System.Reflection.AssemblyKeyFile(@"")]
#endif

namespace gui_cellcountNative
{

  /// <summary>
  /// The CellCountMatlab class provides a CLS compliant, Object (native) interface to
  /// the MATLAB functions contained in the files:
  /// <newpara></newpara>
  /// P:\DiplomRabota\GUI\MatlabReferences\gui_cellcount.m
  /// </summary>
  /// <remarks>
  /// @Version 1.0
  /// </remarks>
  public class CellCountMatlab : IDisposable
  {
    #region Constructors

    /// <summary internal= "true">
    /// The static constructor instantiates and initializes the MATLAB Runtime instance.
    /// </summary>
    static CellCountMatlab()
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

          string ctfFileName = "gui_cellcount.ctf";

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
    /// Constructs a new instance of the CellCountMatlab class.
    /// </summary>
    public CellCountMatlab()
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
    ~CellCountMatlab()
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
    /// Provides a void output, 0-input Objectinterface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    ///
    public void gui_cellcount()
    {
      mcr.EvaluateFunction(0, "gui_cellcount", new Object[]{});
    }


    /// <summary>
    /// Provides a void output, 1-input Objectinterface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="logfile">Input argument #1</param>
    ///
    public void gui_cellcount(Object logfile)
    {
      mcr.EvaluateFunction(0, "gui_cellcount", logfile);
    }


    /// <summary>
    /// Provides a void output, 2-input Objectinterface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    ///
    public void gui_cellcount(Object logfile, Object expname)
    {
      mcr.EvaluateFunction(0, "gui_cellcount", logfile, expname);
    }


    /// <summary>
    /// Provides a void output, 3-input Objectinterface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    ///
    public void gui_cellcount(Object logfile, Object expname, Object HIPASS)
    {
      mcr.EvaluateFunction(0, "gui_cellcount", logfile, expname, HIPASS);
    }


    /// <summary>
    /// Provides a void output, 4-input Objectinterface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    /// <param name="LOWPASS">Input argument #4</param>
    ///
    public void gui_cellcount(Object logfile, Object expname, Object HIPASS, Object 
                        LOWPASS)
    {
      mcr.EvaluateFunction(0, "gui_cellcount", logfile, expname, HIPASS, LOWPASS);
    }


    /// <summary>
    /// Provides a void output, 5-input Objectinterface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    /// <param name="LOWPASS">Input argument #4</param>
    /// <param name="THRESHOLD">Input argument #5</param>
    ///
    public void gui_cellcount(Object logfile, Object expname, Object HIPASS, Object 
                        LOWPASS, Object THRESHOLD)
    {
      mcr.EvaluateFunction(0, "gui_cellcount", logfile, expname, HIPASS, LOWPASS, THRESHOLD);
    }


    /// <summary>
    /// Provides a void output, 6-input Objectinterface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    /// <param name="LOWPASS">Input argument #4</param>
    /// <param name="THRESHOLD">Input argument #5</param>
    /// <param name="MINREG">Input argument #6</param>
    ///
    public void gui_cellcount(Object logfile, Object expname, Object HIPASS, Object 
                        LOWPASS, Object THRESHOLD, Object MINREG)
    {
      mcr.EvaluateFunction(0, "gui_cellcount", logfile, expname, HIPASS, LOWPASS, THRESHOLD, MINREG);
    }


    /// <summary>
    /// Provides a void output, 7-input Objectinterface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    /// <param name="LOWPASS">Input argument #4</param>
    /// <param name="THRESHOLD">Input argument #5</param>
    /// <param name="MINREG">Input argument #6</param>
    /// <param name="CONFLVL">Input argument #7</param>
    ///
    public void gui_cellcount(Object logfile, Object expname, Object HIPASS, Object 
                        LOWPASS, Object THRESHOLD, Object MINREG, Object CONFLVL)
    {
      mcr.EvaluateFunction(0, "gui_cellcount", logfile, expname, HIPASS, LOWPASS, THRESHOLD, MINREG, CONFLVL);
    }


    /// <summary>
    /// Provides a void output, 8-input Objectinterface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    /// <param name="LOWPASS">Input argument #4</param>
    /// <param name="THRESHOLD">Input argument #5</param>
    /// <param name="MINREG">Input argument #6</param>
    /// <param name="CONFLVL">Input argument #7</param>
    /// <param name="SMIN">Input argument #8</param>
    ///
    public void gui_cellcount(Object logfile, Object expname, Object HIPASS, Object 
                        LOWPASS, Object THRESHOLD, Object MINREG, Object CONFLVL, Object 
                        SMIN)
    {
      mcr.EvaluateFunction(0, "gui_cellcount", logfile, expname, HIPASS, LOWPASS, THRESHOLD, MINREG, CONFLVL, SMIN);
    }


    /// <summary>
    /// Provides a void output, 9-input Objectinterface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    /// <param name="LOWPASS">Input argument #4</param>
    /// <param name="THRESHOLD">Input argument #5</param>
    /// <param name="MINREG">Input argument #6</param>
    /// <param name="CONFLVL">Input argument #7</param>
    /// <param name="SMIN">Input argument #8</param>
    /// <param name="SMAX">Input argument #9</param>
    ///
    public void gui_cellcount(Object logfile, Object expname, Object HIPASS, Object 
                        LOWPASS, Object THRESHOLD, Object MINREG, Object CONFLVL, Object 
                        SMIN, Object SMAX)
    {
      mcr.EvaluateFunction(0, "gui_cellcount", logfile, expname, HIPASS, LOWPASS, THRESHOLD, MINREG, CONFLVL, SMIN, SMAX);
    }


    /// <summary>
    /// Provides a void output, 10-input Objectinterface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    /// <param name="LOWPASS">Input argument #4</param>
    /// <param name="THRESHOLD">Input argument #5</param>
    /// <param name="MINREG">Input argument #6</param>
    /// <param name="CONFLVL">Input argument #7</param>
    /// <param name="SMIN">Input argument #8</param>
    /// <param name="SMAX">Input argument #9</param>
    /// <param name="koef">Input argument #10</param>
    ///
    public void gui_cellcount(Object logfile, Object expname, Object HIPASS, Object 
                        LOWPASS, Object THRESHOLD, Object MINREG, Object CONFLVL, Object 
                        SMIN, Object SMAX, Object koef)
    {
      mcr.EvaluateFunction(0, "gui_cellcount", logfile, expname, HIPASS, LOWPASS, THRESHOLD, MINREG, CONFLVL, SMIN, SMAX, koef);
    }


    /// <summary>
    /// Provides the standard 0-input Object interface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_cellcount(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_cellcount", new Object[]{});
    }


    /// <summary>
    /// Provides the standard 1-input Object interface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="logfile">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_cellcount(int numArgsOut, Object logfile)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_cellcount", logfile);
    }


    /// <summary>
    /// Provides the standard 2-input Object interface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_cellcount(int numArgsOut, Object logfile, Object expname)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_cellcount", logfile, expname);
    }


    /// <summary>
    /// Provides the standard 3-input Object interface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_cellcount(int numArgsOut, Object logfile, Object expname, Object 
                            HIPASS)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_cellcount", logfile, expname, HIPASS);
    }


    /// <summary>
    /// Provides the standard 4-input Object interface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    /// <param name="LOWPASS">Input argument #4</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_cellcount(int numArgsOut, Object logfile, Object expname, Object 
                            HIPASS, Object LOWPASS)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_cellcount", logfile, expname, HIPASS, LOWPASS);
    }


    /// <summary>
    /// Provides the standard 5-input Object interface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    /// <param name="LOWPASS">Input argument #4</param>
    /// <param name="THRESHOLD">Input argument #5</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_cellcount(int numArgsOut, Object logfile, Object expname, Object 
                            HIPASS, Object LOWPASS, Object THRESHOLD)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_cellcount", logfile, expname, HIPASS, LOWPASS, THRESHOLD);
    }


    /// <summary>
    /// Provides the standard 6-input Object interface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    /// <param name="LOWPASS">Input argument #4</param>
    /// <param name="THRESHOLD">Input argument #5</param>
    /// <param name="MINREG">Input argument #6</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_cellcount(int numArgsOut, Object logfile, Object expname, Object 
                            HIPASS, Object LOWPASS, Object THRESHOLD, Object MINREG)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_cellcount", logfile, expname, HIPASS, LOWPASS, THRESHOLD, MINREG);
    }


    /// <summary>
    /// Provides the standard 7-input Object interface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    /// <param name="LOWPASS">Input argument #4</param>
    /// <param name="THRESHOLD">Input argument #5</param>
    /// <param name="MINREG">Input argument #6</param>
    /// <param name="CONFLVL">Input argument #7</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_cellcount(int numArgsOut, Object logfile, Object expname, Object 
                            HIPASS, Object LOWPASS, Object THRESHOLD, Object MINREG, 
                            Object CONFLVL)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_cellcount", logfile, expname, HIPASS, LOWPASS, THRESHOLD, MINREG, CONFLVL);
    }


    /// <summary>
    /// Provides the standard 8-input Object interface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    /// <param name="LOWPASS">Input argument #4</param>
    /// <param name="THRESHOLD">Input argument #5</param>
    /// <param name="MINREG">Input argument #6</param>
    /// <param name="CONFLVL">Input argument #7</param>
    /// <param name="SMIN">Input argument #8</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_cellcount(int numArgsOut, Object logfile, Object expname, Object 
                            HIPASS, Object LOWPASS, Object THRESHOLD, Object MINREG, 
                            Object CONFLVL, Object SMIN)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_cellcount", logfile, expname, HIPASS, LOWPASS, THRESHOLD, MINREG, CONFLVL, SMIN);
    }


    /// <summary>
    /// Provides the standard 9-input Object interface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    /// <param name="LOWPASS">Input argument #4</param>
    /// <param name="THRESHOLD">Input argument #5</param>
    /// <param name="MINREG">Input argument #6</param>
    /// <param name="CONFLVL">Input argument #7</param>
    /// <param name="SMIN">Input argument #8</param>
    /// <param name="SMAX">Input argument #9</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_cellcount(int numArgsOut, Object logfile, Object expname, Object 
                            HIPASS, Object LOWPASS, Object THRESHOLD, Object MINREG, 
                            Object CONFLVL, Object SMIN, Object SMAX)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_cellcount", logfile, expname, HIPASS, LOWPASS, THRESHOLD, MINREG, CONFLVL, SMIN, SMAX);
    }


    /// <summary>
    /// Provides the standard 10-input Object interface to the gui_cellcount MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="HIPASS">Input argument #3</param>
    /// <param name="LOWPASS">Input argument #4</param>
    /// <param name="THRESHOLD">Input argument #5</param>
    /// <param name="MINREG">Input argument #6</param>
    /// <param name="CONFLVL">Input argument #7</param>
    /// <param name="SMIN">Input argument #8</param>
    /// <param name="SMAX">Input argument #9</param>
    /// <param name="koef">Input argument #10</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public Object[] gui_cellcount(int numArgsOut, Object logfile, Object expname, Object 
                            HIPASS, Object LOWPASS, Object THRESHOLD, Object MINREG, 
                            Object CONFLVL, Object SMIN, Object SMAX, Object koef)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_cellcount", logfile, expname, HIPASS, LOWPASS, THRESHOLD, MINREG, CONFLVL, SMIN, SMAX, koef);
    }


    /// <summary>
    /// Provides an interface for the gui_cellcount function in which the input and
    /// output
    /// arguments are specified as an array of Objects.
    /// </summary>
    /// <remarks>
    /// This method will allocate and return by reference the output argument
    /// array.<newpara></newpara>
    /// M-Documentation:
    /// Predefined values for sstat
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return</param>
    /// <param name= "argsOut">Array of Object output arguments</param>
    /// <param name= "argsIn">Array of Object input arguments</param>
    /// <param name= "varArgsIn">Array of Object representing variable input
    /// arguments</param>
    ///
    [MATLABSignature("gui_cellcount", 10, 0, 0)]
    protected void gui_cellcount(int numArgsOut, ref Object[] argsOut, Object[] argsIn, params Object[] varArgsIn)
    {
        mcr.EvaluateFunctionForTypeSafeCall("gui_cellcount", numArgsOut, ref argsOut, argsIn, varArgsIn);
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
