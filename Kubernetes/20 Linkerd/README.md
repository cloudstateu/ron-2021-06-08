<img src="../img/logo.png" alt="Chmurowisko logo" width="200" align="right">
<br><br>
<br><br>
<br><br>

# Linkerd overview 

## LAB Overview

#### In this lab you will create simple app with 3 versions of it, split traffic between version 1 and 2. And fix error which accrue every second request in version 3 

If you do not have *linkerd* cli installed follow next 3 steps:
1. Install the CLI:
```
curl -sL https://run.linkerd.io/install | sh
```
2. Add *linkerd* to ypur path
```
export PATH=$PATH:$HOME/.linkerd2/bin
```
3. Verify the CLI is running
```
linkerd version
```
## Task 1: Configure environment 
In this task prepare kubernetes environment 

You will need:
- Working and Connected kubernetes cluster in your local shell 
- Installed linkerd on local device 

1.  Check if you have linkerd installed and you are connected to k8s 
```
linkerd check --pre
```
2.  Install linkerd on cluster
```
linkerd install | kubectl apply -f -
```
3. Check if everything was installed correctly 
```
linkerd check
```
4. Check if all pods created by linkerd are ready    
```
kubectl -n linkerd get deploy
```

## Task 2 Deploy services
In this task you will deploy services in 2 version, route trafic between them.
Them you deploy third version which return error every second request  
1. Create namespace for services 
```
kubectl create ns simple-service
```

2.  Deploy 1 and 2 version of app 
```
kubectl apply -f 1_deployment.yaml -n simple-service
```
3. Open linkerd dashboard. Do this in second terminal. With that you will be have access Web UI whole time  
```
linkerd dashboard
```
4. Create pod where you can connect and run debug commands 
```
kubectl apply -f 2_debug.yaml
```
5. Get all pods 
```
kubectl get pods
```
You should get response like this 
```
NAME                     READY   STATUS    RESTARTS   AGE
debug-5df9f65b74-w8x9s   2/2     Running   0          62m
```
6. Connect to pod 

 **Change debug-5df9f65b74-w8x9s to your pod name**
```
kubectl exec -it debug-5df9f65b74-w8x9s -- /bin/bash
```


7. Install curl package on pod 
   
   *Execute this command on your debug pod* 
```
apt update && apt install curl  
```
8. Check if version 1 and version 2 of your app is working
   
    *Execute this command on your debug pod* 
```
for i in {1..10};do curl versions-app1.simple-service.svc.cluster.local:5000; echo; done
for i in {1..10};do curl versions-app2.simple-service.svc.cluster.local:5000; echo; done
```
9. Inject linkerd to your debug pod 
```
kubectl get deploy -o yaml | linkerd inject - | kubectl apply -f -
```
10. Split traffic from version 1 to version 2 in 80/20 (80% responses is version 1 20% is version 2 )
```
kubectl apply -f 3_split.yaml
```
11. Check if traffic is split
    
    *Execute this command on your debug pod* 
```
for i in {1..10};do curl versions-app1.simple-service.svc.cluster.local:5000; echo; done
```

You should get mostly response form app1 but couple form app2

12.  Deploy third app version with error 
```
kubectl apply -f 4_deployment.yaml -n simple-service
```
13. Check if application return error every second request
 *Execute this command on your debug pod*  
```
for i in {1..10};do curl versions-app-error.simple-service.svc.cluster.local:5000; echo; done
```
14. Add Retryable option to your app version with error. With that if application return error linkerd will automatically make second request 
```
kubectl apply -f 6_profile.yaml
```
15. Check if Retryable option work. You should not get any errors 
```
for i in {1..10};do curl versions-app-error.simple-service.svc.cluster.local:5000; echo; done
```
## END LAB

<br><br>

<center><p>&copy; 2019 Chmurowisko Sp. z o.o.<p></center>
