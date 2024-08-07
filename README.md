## RKL.Sysman.Logging PS Module.
Simple PS Module to handle the data for logging to Sysman.

### Cmdlets
#### Import the module
~~~ powershell
# Import module from where it is stored
PS> Import-Module RKL.LSysman.Logging.dll
~~~

#### Create logging object 
~~~ powershell
PS> $logObject = New-SysmanLogMessage -Source "MyScriptXYZ" -Method "Script" -Version "0.5.6"
PS> $logObject
ActionId      :
Source        : MyScriptXYZ
Method        : Script
MethodVersion : 0.5.6
Status        : None
Text          :
~~~

#### Add verbose log message
~~~ powershell
PS> $logObject | Add-SysmanVerboseMessage "Testing verbose message"
~~~
or
~~~ powershell
PS> Add-SysmanVerboseMessage -Message "Testing verbose message" -LogObject $logObject
~~~

#### Add  warning message
~~~ powershell
PS> $logObject | Add-SysmanWarningMessage "Testing warning message"
~~~
or
~~~ powershell
PS> Add-SysmanWarningMessage -Message "Testing warning message" -LogObject $logObject
~~~

#### Add error log message
~~~ powershell
PS> $logObject | Add-SysmanErrorMessage "Testing error message"
~~~
or
~~~ powershell
PS> Add-SysmanErrorMessage -Message "Testing error message" -LogObject $logObject
~~~

#### Create body for sysman request to add log entry
~~~ powershell
PS> $logObject | ConvertTo-SysmanLogRequestBody
{
  "Text": "[VERBOSE] 2024-08-07 10:21:08.030 Testing verbose message\n[WARNING] 2024-08-07 10:21:20.124 Testing warning message\n[ERROR] 2024-08-07 10:21:32.538 Testing error message",
  "Status": "Failed",
  "Source": "MyScriptXYZ",
  "Method": "Script",
  "MethodVersion": "0.5.6",
  "ActionId": null
}
~~~
or
~~~ powershell
PS> ConvertTo-SysmanLogRequestBody -LogObject $logObject
{
  "Text": "[VERBOSE] 2024-08-07 10:21:08.030 Testing verbose message\n[WARNING] 2024-08-07 10:21:20.124 Testing warning message\n[ERROR] 2024-08-07 10:21:32.538 Testing error message",
  "Status": "Failed",
  "Source": "MyScriptXYZ",
  "Method": "Script",
  "MethodVersion": "0.5.6",
  "ActionId": null
}
~~~
### Properties
#### ```Text```
This property will be constructed from all log messages
#### ```Status```
This property will use the worst severety from all the log messages added.

Possible values:
 - ```None``` No messages added to *LogObject*.
 - ```Completed``` Verbose messages present, no error or warning messages.
 - ```CompletedWithErrors``` At least one warning message and no error messages.
 - ```Failed``` At least one error message present.
 
 #### ```Source```, ```Method``` and ```MethodVersion```
 Taken from the creation parameters.
 
 #### ```ActionId```
 If you use ```-ActionId <long>``` as a parameter to ```New-SysmanLogMessage``` it will be included. Otherwise it will be ```null```.

### Misc

#### Note about pipeline
The cmdlets ```Add-SysmanVerboseMessage```, ```Add-SysmanWarningMessage``` and ```Add-SysmanErrorMessage``` will, unless they are last command in the pipeline write the LogObject to the pipeline so that you can stack them.

~~~ powershell
PS> $logObject | Add-SysmanVerbose "Normal verbosee message"
~~~
