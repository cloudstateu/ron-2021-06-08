<img src="../../../img/logo.png" alt="Chmurowisko logo" width="200" align="right">
<br><br>
<br><br>
<br><br>

# AKS and Azure Key Vault

## LAB Overview

In this lab you are going to integrate your K8s cluster with Azure KeyVault. Azure Key Vault is a service used to store sensitive data, eg passwords, secrets and certificates.

This solution uses user-assigned managed identity that is attached to AKS cluster and has correct access rights to Azure Key Vault. 

What is more, the solution uses Azure CSI driver (https://github.com/Azure/secrets-store-csi-driver-provider-azure) for mounting key vault secrets as volumes. Container Storage Interface (CSI) is a standard for exposing arbitrary block and file storage systems to containerized workloads on Container Orchestration Systems (COs) like Kubernetes. (https://kubernetes-csi.github.io/docs/)

## Prerequisities:
- helm 3 installed (if you don't have helm you can use Azure Cloud Shell - it is already installed there)

## Task 1: Create Azure Key Vault
First we need to create Azure Key Vault service and store some sensitive information in it.

1. Login to Azure portal: `https://portal.azure.com`
2. Click **Create a resource** button
3. Search for **Key Vault** service, click **Create**
4. Fill the form:
   - **Resource group:** use your own resource group
   - **Key vault name:** choose your name
   - **Region:** West Europe
   - **Pricing Tier:** Standard
5. Go **Next** until last blade. Leave all fields by default.
6. Click **Create** and wait for the deployment to finish

## Task 2: Create custom secret in yout key vault
1. Open your Azure Key Vault service.
2. From the left pane select "Secrets".
3. Click "Generate/Import" button
4. Fill the form:
   - **Upload options:** Manual
   - **Name:** choose your secret's name
   - **Value:** enter your secret's custom value
   - leave all fields by default
5. Click **Create**
6. You should see your new secret on secrets list.

## Task 3: Install Azure CSI driver
Now we are going install and Azure CSI driver.
1. Add helm repo:
   
`helm repo add csi-secrets-store-provider-azure https://raw.githubusercontent.com/Azure/secrets-store-csi-driver-provider-azure/master/charts`

2. Install CSI driver:
   
`helm install csi csi-secrets-store-provider-azure/csi-secrets-store-provider-azure`

3. Verify the driver was installed successfully. Enter:
   
```kubectl get pods```

and look for two pods with "csi" prefix
## Task 4: Assign proper access rights for Managed Identity to Key Vault
Now we need to assign **Reader** role for your identity to KeyVault.

1. Obtain aks Managed Identity ID:

```az aks show -g <resource group> -n <aks cluster name> --query identityProfile.kubeletidentity.clientId -o tsv```

> Take note of this ID for later use. 

2. Go to Azure Portal and select your key vault service.
3. Click **Access Policies** from left menu.
4. Click **Add access policy**
5. From **Secret permissions** select list, check **Get**
6. Click **Select principal** and in the search box enter your cluster's managed identity.
7. Click on your identity and then **Select** button.

## Task 5: Configure Secrets Provider
Now we need to configure the Azure CSI driver. 

1. Edit the [secret-provider](./files/secret-provider.yaml) file and set those values:

- useVMManagedIdentity: "true" -> specify that you want to user Managed Identity
- userAssignedIdentityID: "MANAGED_IDENTITY_ID"
- keyvaultName: "NAME_OF_YOUR_KEY_VAULT"
- objectName: NAME_OF_YOUR_SECRET
- tenantId: "YOUR_TENANT_ID" -> can be read from Azure Portal, Key Vault overview page (Directory ID)

2. Apply the config: 
 ```
 kubectl apply -f secret-provider.yaml
 ```

## Task 6: Mount key vault secret as a volume in a pod
1. Create a sample pod with csi volume mounted. Use [pod-csi](./files/pod-csi.yaml) file:
```
kubectl apply -f pod-csi.yaml
```
2. Verify that the file with secret exists:
```
kubectl exec nginx-secrets-store-inline -- ls /mnt/secrets-store/
```

3. Verify the content of the file:
```
kubectl exec nginx-secrets-store-inline -- cat /mnt/secrets-store/SECRET_1

```

## END LAB

<br><br>

<center><p>&copy; 2019 Chmurowisko Sp. z o.o.<p></center>
