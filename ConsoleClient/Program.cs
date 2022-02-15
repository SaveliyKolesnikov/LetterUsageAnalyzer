// See https://aka.ms/new-console-template for more information
using VersOne.Epub;

Console.WriteLine("Hello, World!");
EpubBook epubBook = EpubReader.ReadBook("pelevin_iphuck-10_499568_fb2.epub");

EpubContent bookContent = epubBook.Content;
foreach (EpubTextContentFile htmlFile in bookContent.Html.Values)
{
    string htmlContent = htmlFile.Content;
}