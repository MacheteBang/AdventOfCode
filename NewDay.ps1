param(
    [Parameter(Mandatory)]
    [string]$Year,

    [Parameter(Mandatory)]
    [int]$Day
)

$fullDay = "Day$($Day.ToString('00'))"

Set-Location "./$Year"
if (!(Test-Path "./$fullDay")) {
    dotnet new console --name $fullDay
    dotnet sln add $fullDay

    Set-Location $fullDay
    New-Item "input.txt"
    New-Item "input-test.txt"

    $xml = [xml](Get-Content ".\$fullDay.csproj")

    [xml]$newNode = @"
    <ItemGroup>
        <None Include='input*.txt' CopyToOutputDirectory='Always' />
    </ItemGroup>
"@
    $xml.Project.InsertAfter($xml.ImportNode($newNode.ItemGroup, $true), $xml.Project.PropertyGroup)
    $xml.Save((Resolve-Path ".\$($fullDay).csproj"))

    git add *
    git commit -a -m "Init $Year-$($Day.ToString("00"))"
    git push
}