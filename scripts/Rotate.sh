# Rotation script below will work as a github action

export group=warehouseautomationapi
export functionapp=warehouseAutomationAPI
export webapp=nvmwarehouseautomation

export val=`az functionapp keys set  -n $functionapp -g $group --key-type systemKeys --key-name k1 | jq '.value'| tr -d '"'`

az webapp config appsettings set  --name $webapp -g $group --settings HOSTKEY=$val
