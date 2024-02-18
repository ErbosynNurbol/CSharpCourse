using HtmlAgilityPack;

namespace COMMON
{
    public class HtmlAgilityPackHelper
    {

        #region Convert Html +ConvertHtmlTextNode(string html, string language, string userAgent, string qUrl)
        public static string ConvertHtmlTextNode(string html, string language, string userAgent, string qUrl)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            HtmlNode htmlLangNode = document.DocumentNode.SelectSingleNode("/html");
            if (htmlLangNode != null)
            {
                htmlLangNode.SetAttributeValue("lang", "kk");
            }

            HtmlNodeCollection staticTextNodes = document.DocumentNode.SelectNodes("//*[contains(@rel,'qar-static-text')]/text()[normalize-space(.) != '']");
            HtmlNodeCollection scriptTextNodes = document.DocumentNode.SelectNodes("//script/text()[normalize-space(.) != '']");
            HtmlNodeCollection textNodes = document.DocumentNode.SelectNodes("//text()[normalize-space(.) != '']");
            if (textNodes != null)
            {
                foreach (HtmlNode node in textNodes)
                {
                    if (staticTextNodes != null && staticTextNodes.Contains(node)) continue;
                    if (scriptTextNodes != null && scriptTextNodes.Contains(node)) continue;
                    string innerHtml = System.Net.WebUtility.HtmlDecode(node.InnerHtml);
                    switch (language)
                    {
                        case "tote": { node.InnerHtml = Cyrl2ToteHelper.Cyrl2Tote(innerHtml); } break;
                        case "latyn": { node.InnerHtml = ConvertHelper.Cyrl2Latyn(innerHtml); } break;
                    }
                }
            }

            HtmlNodeCollection inputNodes = document.DocumentNode.SelectNodes("//input[contains(@type,'text')]|//textarea");
            if (inputNodes != null)
            {
                foreach (HtmlNode node in inputNodes)
                {
                    string placeholder = node.Attributes["placeholder"] != null ? node.Attributes["placeholder"].Value : string.Empty;
                    if (string.IsNullOrEmpty(placeholder) || string.IsNullOrEmpty(placeholder = placeholder.Trim())) continue;
                    switch (language)
                    {
                        case "tote": { placeholder = Cyrl2ToteHelper.Cyrl2Tote(placeholder); } break;
                        case "latyn": { placeholder = ConvertHelper.Cyrl2Latyn(placeholder); } break;
                    }
                    node.SetAttributeValue("placeholder", placeholder);
                }
            }

            HtmlNodeCollection textareaNodes = document.DocumentNode.SelectNodes("//textarea");
            if (textareaNodes != null)
            {
                foreach (HtmlNode node in textareaNodes)
                {
                    string innerHtml = System.Net.WebUtility.HtmlDecode(node.InnerHtml);
                    switch (language)
                    {
                        case "tote": { node.InnerHtml = Cyrl2ToteHelper.Cyrl2Tote(innerHtml); } break;
                        case "latyn": { node.InnerHtml = ConvertHelper.Cyrl2Latyn(innerHtml); } break;
                    }
                }
            }


            HtmlNodeCollection metaNodes = document.DocumentNode.SelectNodes(@"//meta[contains(@name,'keywords')]|//meta[contains(@name,'description')]|//meta[contains(@name,'title')]|//meta[contains(@name,'site_name')]
                                                                                   |//meta[contains(@property,'description')]|//meta[contains(@property,'title')]|//meta[contains(@property,'site_name')]");
            if (metaNodes != null)
            {
                foreach (HtmlNode node in metaNodes)
                {
                    string content = node.Attributes["content"] != null ? node.Attributes["content"].Value : string.Empty;
                    if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(content = content.Trim())) continue;
                    switch (language)
                    {
                        case "tote": { content = Cyrl2ToteHelper.Cyrl2Tote(content); } break;
                        case "latyn": { content = ConvertHelper.Cyrl2Latyn(content); } break;
                    }
                    node.SetAttributeValue("content", content);
                }
            }

            HtmlNodeCollection imgNodes = document.DocumentNode.SelectNodes(@"//img");
            if (imgNodes != null)
            {
                foreach (HtmlNode node in imgNodes)
                {
                    string alt = node.Attributes["alt"] != null ? node.Attributes["alt"].Value : string.Empty;
                    string dataCopyright = node.Attributes["data-copyright"] != null ? node.Attributes["data-copyright"].Value : string.Empty;
                    switch (language)
                    {
                        case "tote": { alt = Cyrl2ToteHelper.Cyrl2Tote(alt); dataCopyright = Cyrl2ToteHelper.Cyrl2Tote(dataCopyright); } break;
                        case "latyn": { alt = ConvertHelper.Cyrl2Latyn(alt); dataCopyright = ConvertHelper.Cyrl2Latyn(dataCopyright); } break;
                    }
                    node.SetAttributeValue("alt", alt);
                    if (!string.IsNullOrEmpty(dataCopyright))
                    {
                        node.SetAttributeValue("data-copyright", dataCopyright);
                    }
                }
            }


            HtmlNodeCollection staticANodes = document.DocumentNode.SelectNodes("//a[contains(@rel,'ankui-static-text')]");
            HtmlNodeCollection aNodes = document.DocumentNode.SelectNodes("//a");
            if (aNodes != null)
            {
                foreach (HtmlNode node in aNodes)
                {
                    if (staticANodes != null && staticANodes.Contains(node)) continue;
                    string href = node.Attributes["href"] != null ? node.Attributes["href"].Value : string.Empty;
                    if (string.IsNullOrEmpty(href) || string.IsNullOrEmpty(href = href.Trim())) continue;
                    if (href.Substring(0, 1).Equals("/"))
                    {
                        node.SetAttributeValue("href", qUrl + href);
                    }
                }
            }

            return document.DocumentNode.OuterHtml;
        }

        #endregion

        #region HTML ішіндегі Body-дың мазмұнын алу +GetHtmlBoyInnerHtml(string html)
        public static string GetHtmlBoyInnerHtml(string html)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            HtmlNode bodyNode = document.DocumentNode.SelectSingleNode("//body");
            if (bodyNode != null)
            {
                return bodyNode.InnerHtml;
            }

            return html;
        }
        #endregion
    }
}