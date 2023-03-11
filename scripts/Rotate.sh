# 1. Create a service principal in Azure:

export group=warehouseautomationapi
export subscription=c9e7aaf4-2cb3-40e0-b1dd-54276cbdfd8b

az ad sp create-for-rbac --name nvmsp1 --role "contributor" --scopes /subscriptions/$subscription/resourceGroups/$group --sdk-auth




# Rotation script, should be automated

export val=`az functionapp keys set  -n warehouseAutomationAPI -g $group --key-type systemKeys --key-name k1 | jq '.value'| tr -d '"'`

az webapp config appsettings set  --name nvmwarehouseautomation -g $group --settings HOSTKEY=$val
