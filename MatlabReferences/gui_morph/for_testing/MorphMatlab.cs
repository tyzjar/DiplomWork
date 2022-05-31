/*
* MATLAB Compiler: 8.1 (R2020b)
* Date: Tue May 31 13:56:47 2022
* Arguments:
* "-B""macro_default""-W""dotnet:gui_morph,MorphMatlab,4.0,private,version=1.0""-T""link:l
* ib""-d""P:\DiplomRabota\GUI\MatlabReferences\gui_morph\for_testing""-v""class{MorphMatla
* b:P:\DiplomRabota\GUI\MatlabReferences\gui_morph.m}"
*/
using System;
using System.Reflection;
using System.IO;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

#if SHARED
[assembly: System.Reflection.AssemblyKeyFile(@"")]
#endif

namespace gui_morph
{

  /// <summary>
  /// The MorphMatlab class provides a CLS compliant, MWArray interface to the MATLAB
  /// functions contained in the files:
  /// <newpara></newpara>
  /// P:\DiplomRabota\GUI\MatlabReferences\gui_morph.m
  /// </summary>
  /// <remarks>
  /// @Version 1.0
  /// </remarks>
  public class MorphMatlab : IDisposable
  {
    #region Constructors

    /// <summary internal= "true">
    /// The static constructor instantiates and initializes the MATLAB Runtime instance.
    /// </summary>
    static MorphMatlab()
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

          string ctfFileName = "gui_morph.ctf";

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
    /// Constructs a new instance of the MorphMatlab class.
    /// </summary>
    public MorphMatlab()
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
    ~MorphMatlab()
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
    /// Provides a void output, 0-input MWArrayinterface to the gui_morph MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// folder - path to the experiment folder
    /// order - cell 2xN order{i,1}{j} brains are to be morphed to order{i,2}
    /// </remarks>
    ///
    public void gui_morph()
    {
      mcr.EvaluateFunction(0, "gui_morph", new MWArray[]{});
    }


    /// <summary>
    /// Provides a void output, 1-input MWArrayinterface to the gui_morph MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// folder - path to the experiment folder
    /// order - cell 2xN order{i,1}{j} brains are to be morphed to order{i,2}
    /// </remarks>
    /// <param name="logfile">Input argument #1</param>
    ///
    public void gui_morph(MWArray logfile)
    {
      mcr.EvaluateFunction(0, "gui_morph", logfile);
    }


    /// <summary>
    /// Provides a void output, 2-input MWArrayinterface to the gui_morph MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// folder - path to the experiment folder
    /// order - cell 2xN order{i,1}{j} brains are to be morphed to order{i,2}
    /// </remarks>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    ///
    public void gui_morph(MWArray logfile, MWArray expname)
    {
      mcr.EvaluateFunction(0, "gui_morph", logfile, expname);
    }


    /// <summary>
    /// Provides a void output, 3-input MWArrayinterface to the gui_morph MATLAB
    /// function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// folder - path to the experiment folder
    /// order - cell 2xN order{i,1}{j} brains are to be morphed to order{i,2}
    /// </remarks>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="varargin">Array of MWArrays representing the input arguments 3
    /// through varargin.length+2</param>
    ///
    public void gui_morph(MWArray logfile, MWArray expname, params MWArray[] varargin)
    {
      MWArray[] args = {logfile, expname};
      int nonVarargInputNum = args.Length;
      int varargInputNum = varargin.Length;
      int totalNumArgs = varargInputNum > 0 ? (nonVarargInputNum + varargInputNum) : nonVarargInputNum;
      Array newArr = Array.CreateInstance(typeof(MWArray), totalNumArgs);

      int idx = 0;

      for (idx = 0; idx < nonVarargInputNum; idx++)
        newArr.SetValue(args[idx],idx);

      if (varargInputNum > 0)
      {
        for (int i = 0; i < varargInputNum; i++)
        {
          newArr.SetValue(varargin[i], idx);
          idx++;
        }
      }

      mcr.EvaluateFunction(0, "gui_morph", (MWArray[])newArr );
    }


    /// <summary>
    /// Provides the standard 0-input MWArray interface to the gui_morph MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// folder - path to the experiment folder
    /// order - cell 2xN order{i,1}{j} brains are to be morphed to order{i,2}
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_morph(int numArgsOut)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_morph", new MWArray[]{});
    }


    /// <summary>
    /// Provides the standard 1-input MWArray interface to the gui_morph MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// folder - path to the experiment folder
    /// order - cell 2xN order{i,1}{j} brains are to be morphed to order{i,2}
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="logfile">Input argument #1</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_morph(int numArgsOut, MWArray logfile)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_morph", logfile);
    }


    /// <summary>
    /// Provides the standard 2-input MWArray interface to the gui_morph MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// folder - path to the experiment folder
    /// order - cell 2xN order{i,1}{j} brains are to be morphed to order{i,2}
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_morph(int numArgsOut, MWArray logfile, MWArray expname)
    {
      return mcr.EvaluateFunction(numArgsOut, "gui_morph", logfile, expname);
    }


    /// <summary>
    /// Provides the standard 3-input MWArray interface to the gui_morph MATLAB function.
    /// </summary>
    /// <remarks>
    /// M-Documentation:
    /// folder - path to the experiment folder
    /// order - cell 2xN order{i,1}{j} brains are to be morphed to order{i,2}
    /// </remarks>
    /// <param name="numArgsOut">The number of output arguments to return.</param>
    /// <param name="logfile">Input argument #1</param>
    /// <param name="expname">Input argument #2</param>
    /// <param name="varargin">Array of MWArrays representing the input arguments 3
    /// through varargin.length+2</param>
    /// <returns>An Array of length "numArgsOut" containing the output
    /// arguments.</returns>
    ///
    public MWArray[] gui_morph(int numArgsOut, MWArray logfile, MWArray expname, params 
                         MWArray[] varargin)
    {
      MWArray[] args = {logfile, expname};
      int nonVarargInputNum = args.Length;
      int varargInputNum = varargin.Length;
      int totalNumArgs = varargInputNum > 0 ? (nonVarargInputNum + varargInputNum) : nonVarargInputNum;
      Array newArr = Array.CreateInstance(typeof(MWArray), totalNumArgs);

      int idx = 0;

      for (idx = 0; idx < nonVarargInputNum; idx++)
        newArr.SetValue(args[idx],idx);

      if (varargInputNum > 0)
      {
        for (int i = 0; i < varargInputNum; i++)
        {
          newArr.SetValue(varargin[i], idx);
          idx++;
        }
      }

      return mcr.EvaluateFunction(numArgsOut, "gui_morph", (MWArray[])newArr );
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
