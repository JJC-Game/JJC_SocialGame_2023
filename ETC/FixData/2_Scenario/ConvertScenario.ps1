$scriptPath = Get-Location
$excelFileName = "Scenario_VBAManager.xlsm"
$excelFunctionName = "OutputScenario"
$excelPath = Join-Path $scriptPath $excelFileName
$targetExcelFileName = ""
$outputDirectoryPath = Join-Path $scriptPath "..\1_Output_Temp"
$assetPath = Join-Path $scriptPath "..\..\..\Assets\Resources\FixData\Scenario"
if(Test-Path $excelPath){
    # Excel�I�u�W�F�N�g���擾
    $excel = New-Object -ComObject Excel.Application

            
    for ($i=1; $i -lt 16; $i++){

        $targetScenarioName = "Scenario" + "{0:000}" -f $i
        $targetExcelFileName = $targetScenarioName + ".xlsm"
        $targetExcelPath = Join-Path $scriptPath $targetExcelFileName

        $writeString = $excelFileName + "�̊֐�" + $excelFunctionName + "��" + $targetExcelFileName + "�����ɌĂяo���Ă��܂�."
        Write-Output $writeString
    

        try
        {
            # Excel�t�@�C����OPEN
            $book_target = $excel.Workbooks.Open($targetExcelPath)

            # Excel�t�@�C����OPEN
            $book_VBA = $excel.Workbooks.Open($excelPath)

            # �v���V�[�W�������s
            $excel.Run($excelFunctionName)

            # Excel�t�@�C����CLOSE
            $book_VBA.Close()

            # Excel�t�@�C����CLOSE
            $book_target.Close()
        }
        catch
        {
            $ws = New-Object -ComObject Wscript.Shell
            $ws.popup("�G���[ : " + $PSItem)
        }
        finally
        {
            $writeString = $excelFileName + "�̊֐�" + $excelFunctionName + "�𐳏�I��."
            Write-Output $writeString

            #���ʕ���utf-8�ɕϊ�����.
            $sourceFileName = $targetExcelFileName + "_Scenario.csv"
            $sourcePath = Join-Path $outputDirectoryPath $sourceFileName
            $allText = Get-Content $sourcePath -Encoding default
            Write-Output $allText | Out-File $sourcePath -Encoding UTF8
            $writeString = $sourceFileName + "��Shift_JIS����utf-8(BOM�t��)�ɕϊ����܂���."
            Write-Output $writeString
        
            #���ʕ���Asset�ȉ��Ɉړ�.
            $destPath = $assetPath
            if(Test-Path $destPath){
                Copy-Item -Path $sourcePath -Destination $destPath -Force
                $writeString = $sourceFileName + "��" + $destPath + "�ɔz�u���܂���."
                Write-Output $writeString
            }else{
                $writeString = $destPath + "������܂���. ERROR!!!!!"
                Write-Error $writeString
                Read-Host "Enter�L�[�ŏI��"
            }
        }
    }

    # Excel���I��
    $excel.Quit()
    [System.Runtime.InteropServices.Marshal]::FinalReleaseComObject($excel) | Out-Null
    

}else{
    $writeString = $excelPath + "������܂���. ERROR!!!!!"
    Write-Error $writeString
    Read-Host "Enter�L�[�ŏI��"
}