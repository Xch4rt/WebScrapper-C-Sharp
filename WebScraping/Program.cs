using System;
using System.Collections.Generic;
using System.Configuration;
using PuppeteerSharp;

string url = ConfigurationManager.AppSettings["url"];
string path = ConfigurationManager.AppSettings["chrome_path"];

using var browserFetcher = new BrowserFetcher();
await browserFetcher.DownloadAsync();

await using var browser = await Puppeteer.LaunchAsync(
    new LaunchOptions
    {
        Headless = true,
        ExecutablePath = path,
    }
    );

await using var page = await browser.NewPageAsync();


await page.GoToAsync(url);

List<string> data = new List<string>();

var result = await page.EvaluateFunctionAsync("()=>{"+
    "const a = document.querySelectorAll('.product-name');" +
    "const res = [];"+
    "for (let i = 0; i < a.length; i++)"+
    "   res.push(a[i].innerHTML);"+
    "return res;}");

foreach (var item in result)
    Console.WriteLine(item);
