# EFK stack

## LAB Overview

#### In this lab you will deploy EFK stack

## Task1: Deploy elasticsearch
1. Create new namespace:
``kubectl create -f kube-logging.yaml``

2. Check if created:
``kubectl get namespace``

3. Create elasticsearch service:
``kubectl create -f elasticsearch_svc.yaml``

4. Check if created:
``kubectl get svc --namespace kube-logging``

5. Create elasticsearch stateful set:
``kubectl create -f elasticsearch_statefulset.yaml``

6. Wait for the depoyment:
``kubectl rollout status sts/es-cluster --namespace=kube-logging``

7. Check if ES is working:
   
``kubectl port-forward es-cluster-0 9200:9200 --namespace=kube-logging``

``curl http://localhost:9200/_cluster/state?pretty``

## Task2: Deploy kibana

1. Create service:
``kubectl create -f kibana-service.yaml``

2. Create deployment:
``kubectl create -f kibana-deployment.yaml``

3. Check deployment status:
``kubectl rollout status deployment/kibana --namespace=kube-logging``

4. Check kibana pod:
``kubectl get pods --namespace=kube-logging``

5. Forward port to kibana:
``kubectl port-forward <KIBANA POD> 5601:5601 --namespace=kube-logging``

## Task3: Deploy fluentd daemon set
1. Deploy:
``kubectl create -f fluentd.yaml``

2. Create simple logging container:
``kubectl create -f counter.yaml``

3. Search in kibana for metrics:
"kubernetes.pod_name:counter"