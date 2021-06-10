
1. A list of available StorageClass
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

1. PersistanceVolumeClaim with Azure Files  
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
