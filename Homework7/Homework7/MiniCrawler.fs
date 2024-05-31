open System.Net.Http
open System.Text.RegularExpressions

type PageInfo =
    {
        URL: string
        Size: int;
    }
    
/// <summary>
/// Method to download page's content
/// </summary>
/// <param name="url">Link to the page</param>
let downloadPageAsync (url: string) =
    async {
        use client = new HttpClient()
        let! content = client.GetStringAsync(url) |> Async.AwaitTask
        return url, content
    }

/// <summary>
/// Method to find all links on the page
/// </summary>
/// <param name="html">HTML format of page</param>
let findLinks (html: string) =
    let regex = Regex(@"<a\s+href=""(https://[^""]+)"">", RegexOptions.IgnoreCase)
    
    [ for m in regex.Matches(html) -> m.Groups[1].Value ]
    

/// <summary>
/// Method to find all links on the page and size of its content
/// </summary>
/// <param name="url">Link to the main page</param>
let crawlerWork (url: string) =
    async {
        let! (mainUrl, mainContent) = downloadPageAsync url

        let links = findLinks mainContent
        
        let! pages = 
            match links with
            | [] -> 
                printfn "No URLs found"
                async.Return [||]
            | _ ->
                printfn $"Found %d{List.length links} URLs"
                links 
                |> List.map downloadPageAsync 
                |> Async.Parallel
        
        let report = 
            pages 
            |> Array.map (fun (link, content) -> {URL = link; Size = content.Length})
            |> List.ofArray

        return report
    }
