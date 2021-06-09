
1. Make sure your subscription has a registered provider for Storage:
```
az provider register --namespace 'Microsoft.Storage'
```

2. A list of available StorageClass
```
kubectl get sc
```

3. StorageClass details 
```
kubectl describe sc default
```
```
kubectl describe sc managed-premium
```

4. StorageClass for Azure Files  
```yaml
kind: StorageClass
apiVersion: storage.k8s.io/v1
metadata:
  name: azurefile
provisioner: kubernetes.io/azure-file
mountOptions:
  - dir_mode=0777
  - file_mode=0777
  - uid=1000
  - gid=1000
  - mfsymlinks
  - nobrl
  - cache=none
parameters:
  skuName: Standard_LRS
  ```
5. CustomRole & Binding for ServiceAccount  
```yaml
---
apiVersion: rbac.authorization.k8s.io/v1
kind: ClusterRole
metadata:
  name: system:azure-cloud-provider
rules:
- apiGroups: ['']
  resources: ['secrets']
  verbs:     ['get','create']
---
apiVersion: rbac.authorization.k8s.io/v1
kind: ClusterRoleBinding
metadata:
  name: system:azure-cloud-provider
roleRef:
  kind: ClusterRole
  apiGroup: rbac.authorization.k8s.io
  name: system:azure-cloud-provider
subjects:
- kind: ServiceAccount
  name: persistent-volume-binder
  namespace: kube-system
```

6. PersistanceVolumeClaim with Azure Files  
```yaml
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: azurefile
spec:
  accessModes:
    - ReadWriteMany
  storageClassName: azurefile
  resources:
    requests:
      storage: 10Gi
```

7. Deployment - Volume Azure Files  
```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: netcoresample
  labels:
    app: sample
spec:
  replicas: 2
  selector:
    matchLabels:
      app: sample
  template:
    metadata:
      labels:
        app: sample
    spec:
      containers:
      - name: netcoresample
        image: mcr.microsoft.com/dotnet/core/samples:aspnetapp
        ports:
        - containerPort: 80
        volumeMounts:
         - mountPath: "/mnt/test"
           name: volume
      volumes:
        - name: volume
          persistentVolumeClaim:
            claimName: azurefile
```

8. Create new file in Azure Files (using Portal).
Use kubectl exec to get a shell to a running Container.
List files inside '/mnt/test'.'
