1. 
Check if Metric Server is installed in your cluster:
```
kubectl get pods -A
```

You should see the object in namespace 'kube-system' with a name beginning with:
```
metrics-server
```


2. Create deployment from a file`01_deployment.yaml`:
```
kubectl apply -f 01_deployment.yaml
```

3. In a new / different window we set the preview in continuous mode of changes in POD:
```
kubectl get pods -w
```

4. Create HPA for our deployment:
```
kubectl autoscale deployment web --min=1  --max=4 --cpu-percent=30
```  

5. Artificial CPU load simulation:
 - Login to POD from point 1
```
kubectl exec -it <nazwa_pod> /bin/bash
```
 - Package update:
```
apt-get update
```
 - Installation of the load-generating application:
```
apt-get install stress
```
 - Starting load generation:
```
stress -c 5
```

6. In a new / different window we monitor changes in our HPA:
```
kubectl get hpa -w
```
