namespace DMF_ImaGenerator;

using CommandLine;
using CommandLine.Text;

internal class ParserSettings
{
    public static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs)
    {
        HelpText helpText;
        if (errs.IsVersion()) //check if error is version request
            helpText = HelpText.AutoBuild(result);
        else
        {
            helpText = HelpText.AutoBuild(result, h =>
            {
                h.AdditionalNewLineAfterOption = true;
                h.Heading = "DMF-ImaGenerator 0.1.0"; //change header
                h.Copyright = "Copyright (c) 2022 DMF"; //change copyright text
                h.AutoVersion = false;
                h.AutoHelp = false;

                return HelpText.DefaultParsingErrorsHandler(result, h);
            }, e => e, false);
            Console.WriteLine(helpText);
        }
    }

    [Verb("demo", HelpText = "Generates a demo image")]
    public class DemoOptions
    {
        [Option('w', "width", MetaValue = "INT", Default = 480, Separator = ' ',
            HelpText = "The width of the image in pixels." )]
        public int Width { get; set; }
        
        [Option('h', "height", MetaValue = "INT", Default = 480, Separator = ' ',
            HelpText = "The height of the image in pixels.")]
        public int Height { get; set; }
        
        [Option('c', "camera", MetaValue = "BOOL", Default = true, Separator = ' ',
            HelpText = "Whether of not use a Perspective Camera to render the image. If false, a Orthogonal Camera will be used.")]
        public bool Camera { get; set; }
    }

    [Verb("pfm2png", HelpText = "Convert a file from PFM format to PNG format.")]
    internal class Pfm2PngOptions
    {
        [Value(0, MetaName = "PFM", HelpText = "The input file name, path based. Must be a PFM file.", Required = true)]
        public string InputFile { get; set; } = null!;

        [Value(1, MetaName = "PNG", HelpText = "The output file name, path based.", Default = "output_image.png")]
        public string OutputFile { get; set; } = null!;

        [Option('g', "gamma", MetaValue = "FLOAT", Default = 1.0f, Separator = ' ',
            HelpText = "The gamma correction to apply in the image conversion.")]
        public float Gamma { get; set; }

        [Option('f', "factor", MetaValue = "FLOAT", Default = 0.18f, Separator = ' ',
            HelpText = "The factor for the luminosity normalization of the image.")]
        public float Factor { get; set; }
    }
}