$scriptPath = Get-Location
$excelFileName = "Scenario_VBAManager.xlsm"
$excelFunctionName = "OutputScenario"
$excelPath = Join-Path $scriptPath $excelFileName
$targetExcelFileName = ""
$outputDirectoryPath = Join-Path $scriptPath "..\1_Output_Temp"
$assetPath = Join-Path $scriptPath "..\..\..\Assets\Resources\FixData\Scenario"
if(Test-Path $excelPath){
    # Excelオブジェクトを取得
    $excel = New-Object -ComObject Excel.Application

            
    for ($i=1; $i -lt 500; $i++){

        $targetScenarioName = "Scenario" + "{0:000}" -f $i
        $targetExcelFileName = $targetScenarioName + ".xlsm"
        $targetExcelPath = Join-Path $scriptPath $targetExcelFileName

        $writeString = $excelFileName + "の関数" + $excelFunctionName + "を" + $targetExcelFileName + "向けに呼び出しています."
        Write-Output $writeString

        if(Test-Path $targetExcelPath){

        }else{
            continue
        }
    

        try
        {
            # ExcelファイルをOPEN
            $book_target = $excel.Workbooks.Open($targetExcelPath)

            # ExcelファイルをOPEN
            $book_VBA = $excel.Workbooks.Open($excelPath)

            # プロシージャを実行
            $excel.Run($excelFunctionName)

            # ExcelファイルをCLOSE
            $book_VBA.Close()

            # ExcelファイルをCLOSE
            $book_target.Close()
        }
        catch
        {
            $ws = New-Object -ComObject Wscript.Shell
            $ws.popup("エラー : " + $PSItem)
        }
        finally
        {
            $writeString = $excelFileName + "の関数" + $excelFunctionName + "を正常終了."
            Write-Output $writeString

            #成果物をutf-8に変換する.
            $sourceFileName = $targetExcelFileName + "_Scenario.csv"
            $sourcePath = Join-Path $outputDirectoryPath $sourceFileName
            $allText = Get-Content $sourcePath -Encoding default
            Write-Output $allText | Out-File $sourcePath -Encoding UTF8
            $writeString = $sourceFileName + "をShift_JISからutf-8(BOM付き)に変換しました."
            Write-Output $writeString
        
            #成果物をAsset以下に移動.
            $destPath = $assetPath
            if(Test-Path $destPath){
                Copy-Item -Path $sourcePath -Destination $destPath -Force
                $writeString = $sourceFileName + "を" + $destPath + "に配置しました."
                Write-Output $writeString
            }else{
                $writeString = $destPath + "がありません. ERROR!!!!!"
                Write-Error $writeString
                Read-Host "Enterキーで終了"
            }
        }
    }

    # Excelを終了
    $excel.Quit()
    [System.Runtime.InteropServices.Marshal]::FinalReleaseComObject($excel) | Out-Null
    

}else{
    $writeString = $excelPath + "がありません. ERROR!!!!!"
    Write-Error $writeString
    Read-Host "Enterキーで終了"
}