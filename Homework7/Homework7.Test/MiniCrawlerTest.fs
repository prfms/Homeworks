module MiniCrawlerTest
open NUnit.Framework
open FsUnit
open MiniCrawler

[<Test>]
let ``Crawler can find link from the page``() =
    let actualReport = [{URL =  "https://careers.yadro.com/"; Size = 290292}]
    crawlerWork "https://careers.yadro.com/impulse/" |> Async.RunSynchronously |>
        should equal actualReport