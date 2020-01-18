using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateArray : MonoBehaviour
{
    public Slider fixer;
    public Text sort; 
    public int maxnum;
    public GameObject stopbtn;
    public int amount;

    public int[] numbers;

    public bool sorting, stop;

    public float quicksorttime;
    public int quicksortiteration = 0;

    public VisualizeArray varray;
    // Start is called before the first frame update
    void Start()
    {
        sorting = false;
        stop = false;
        amount =(int) fixer.value;
        arraygenerate();
        stopbtn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        varray.visualize();

        if (sorting)
        {
            stopbtn.SetActive(true);
        }
        else
        {
            stopbtn.SetActive(false);
        }
        
    }

    public void arraygenerate()
    {
        if (!sorting)
        {
            numbers = new int[amount];

            for (int i = 0; i < amount; i++)
            {
                int rand = Random.Range(10, maxnum);
                numbers[i] = rand;
            }
            varray.destroyarrayvisualize();
            varray.createvisualizer();
        }
        
    }

    public void setamount()
    {
        if (!sorting)
        {
            varray.destroyarrayvisualize();
            amount =(int) fixer.value;
            arraygenerate();
        }
    }

    public bool isSorted()
    {
        for (int i = 0; i < amount - 1; i++)
        {
            if (numbers[i] > numbers[i+1])
            {
                return false;
            }
        }

        return true;
    }

    public void stopper()
    {
        if(sorting)
            StartCoroutine(stopsort());
    }

    public IEnumerator stopsort()
    {
        stop = true;
        sorting = false;
        sort.text = "Sort stopped";
        yield return new WaitForSeconds(1);

        stop = false;
    }

    public IEnumerator showsorted()
    {
        for (int a = 0; a < amount - 1; a++)
        {
            varray.sqstore[a].GetComponent<SpriteRenderer>().color = Color.green;
            varray.sqstore[a+ 1].GetComponent<SpriteRenderer>().color = Color.green;
                
            yield return new WaitForSeconds(1f/(float)amount);
                
                
            varray.sqstore[a].GetComponent<SpriteRenderer>().color = Color.white;
            varray.sqstore[a+ 1].GetComponent<SpriteRenderer>().color = Color.white;
        }
        sorting = false;
        sort.text = "Sorted";
    }

    public void bubblesortstart()
    {
        StartCoroutine(bubblesort());
    }

    public IEnumerator bubblesort()
    {
        if (!sorting)
        {
            sorting = true;
            sort.text = "Sorting...";
            float starttime = Time.time;
            for (int i = 0; i < amount - 1; i++)
            {
                for (int j = 0; j < amount - i - 1; j++)
                {
                    if (stop == true)    yield break;
                    if (numbers[j] > numbers[j+1])
                    {
                        varray.sqstore[j].GetComponent<SpriteRenderer>().color = Color.blue;
                        varray.sqstore[j+1].GetComponent<SpriteRenderer>().color = Color.blue;
                        int bak = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = bak;
                        
                    }
                    else
                    {
                        varray.sqstore[j].GetComponent<SpriteRenderer>().color = Color.green;
                        varray.sqstore[j+1].GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    //float timeskip = 0.5f * (1f/(500f*((float)amount/10)));
                    float timeskip = 11000f * Mathf.Exp(-amount);
                    yield return new WaitForSeconds(timeskip);
                    varray.sqstore[j].GetComponent<SpriteRenderer>().color = Color.red;
                    varray.sqstore[j+1].GetComponent<SpriteRenderer>().color = Color.red;
                    if (i > 0)
                    {
                        varray.sqstore[amount-i].GetComponent<SpriteRenderer>().color = Color.white;
                        
                    }
                }

            }

                        varray.sqstore[0].GetComponent<SpriteRenderer>().color = Color.white;
                        varray.sqstore[1].GetComponent<SpriteRenderer>().color = Color.white;

            
            StartCoroutine(showsorted());
        }
    }


    //
    //
    //QUICK SORT
    //
    //
    
    public void quicksortstart()
    {
        if (!sorting)
        {
            sorting = true;
            sort.text = "Sorting...";
            quicksortiteration = 0;
            StartCoroutine(quicksort (0, amount-1));
            //quicksort(0, amount - 1);
            
        }
    }

    public IEnumerator quicksort(int i, int k)
    {
        quicksortiteration++;
        yield return new WaitForSeconds(0.005f);
        if (i < k)
        {
            
            int left = i;
            int right = k;

            int pivotIndex = left + (right-left)/2;
            int pivotValue = numbers[pivotIndex];
            int temp = numbers[pivotIndex];
            numbers[pivotIndex] = numbers[right];
            numbers[right] = temp;
            int storeIndex = left;
            for (int a=left; a<right; a++){
                yield return new WaitForSeconds(0.005f);
                if (stop == true) yield break;
                if (numbers[a] < pivotValue){
                    temp = numbers[a];
                    numbers[a] = numbers[storeIndex];
                    numbers[storeIndex] = temp;
                    varray.sqstore[a].GetComponent<SpriteRenderer>().color = Color.magenta;
                    varray.sqstore[storeIndex].GetComponent<SpriteRenderer>().color = Color.blue;
                    storeIndex = storeIndex + 1;

                }
                
            }
                varray.sqstore[left].GetComponent<SpriteRenderer>().color = Color.white;
                varray.sqstore[storeIndex].GetComponent<SpriteRenderer>().color = Color.white;
                if (storeIndex>0)
                {
                    varray.sqstore[storeIndex-1].GetComponent<SpriteRenderer>().color = Color.white;
                    
                }
            
            temp = numbers[storeIndex];
            numbers[storeIndex] = numbers[right];
            numbers[right] = temp;// Move pivot to its final place

            int p = storeIndex;
            
            
            //varray.sqstore[storeIndex].GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(quicksort(i, p - 1));
            StartCoroutine(quicksort(p+1, k));
            if (quicksortiteration >= amount/10)
                varray.sqstore[amount-1].GetComponent<SpriteRenderer>().color = Color.white;

        }
        
        
        
        //if ((float)quicksortiteration >= 1.25f * (float)amount && sorting)
        bool issort = isSorted();
        if (issort)
        {
            
            StartCoroutine(showsorted());
        }
    }
    
    public int quicksortpartition(int l,int u)
    {
        int v,i,j,temp;
        v=numbers[l];
        i=l;
        j=u+1;
	
        do
        {
            do
                i++;
			
            while(numbers[i]<v&&i<=u);
		
            do
                j--;
            while(v<numbers[j]);
		
            if(i<j)
            {
                temp=numbers[i];
                numbers[i]=numbers[j];
                numbers[j]=temp;
            }
        }while(i<j);
	
        numbers[l]=numbers[j];
        numbers[j]=v;
	
        return(j);
    }
    
    
    
    //
    //
    //Merge sort
    //
    //

    public void mergesorter()
    {
        if (!sorting)
        {
            sorting = true;
            sort.text = "Sorting...";
            StartCoroutine(mergeSort ());
        }
    }

    public IEnumerator mergeSort()
    {
        int i,j,k,size,l1,h1,l2,h2;
        int[] temp = new int[amount];
        
        for(size=1; size < amount; size=size*2 )
        {
            l1=0;
            k=0;  /*Index for temp array*/
            while( l1+size < amount)
            {
                h1=l1+size-1;
                l2=h1+1;
                h2=l2+size-1;
                /* h2 exceeds the limlt of arr */
                if( h2>=amount ) 
                    h2=amount-1;
			
                /*Merge the two pairs with lower limits l1 and l2*/
                i=l1;
                j=l2;
			
                while(i<=h1 && j<=h2 )
                {
                    varray.sqstore[i].GetComponent<SpriteRenderer>().color = Color.blue;
                    varray.sqstore[j].GetComponent<SpriteRenderer>().color = Color.blue;
                    if (stop == true) yield break;
                    float timeskip = 11000f * Mathf.Exp(-amount);
                    yield return new WaitForSeconds(timeskip);
                    varray.sqstore[i].GetComponent<SpriteRenderer>().color = Color.red;
                    varray.sqstore[j].GetComponent<SpriteRenderer>().color = Color.red;
                    if( numbers[i] <= numbers[j] )
                        temp[k++]=numbers[i++];
                    else
                        temp[k++]=numbers[j++];
                    
                    
                    
                }

                while (i <= h1)
                {
                    varray.sqstore[i].GetComponent<SpriteRenderer>().color = Color.blue;
                    if (stop == true) yield break;
                    float timeskip = 11000f * Mathf.Exp(-amount);
                    yield return new WaitForSeconds(timeskip);
                    varray.sqstore[i].GetComponent<SpriteRenderer>().color = Color.red;
                    temp[k++]=numbers[i++];
                }

                while (j <= h2)
                {
                    varray.sqstore[j].GetComponent<SpriteRenderer>().color = Color.blue;
                    if (stop == true) yield break;
                    float timeskip = 11000f * Mathf.Exp(-amount);
                    yield return new WaitForSeconds(timeskip);
                    varray.sqstore[j].GetComponent<SpriteRenderer>().color = Color.red;
                    temp[k++]=numbers[j++];
                }
                /**Merging completed**/
                /*Take the next two pairs for merging */
                l1=h2+1; 
            }/*End of while*/

            /*any pair left */
            for(i=l1; k<amount; i++) 
                temp[k++]=numbers[i];

            for(i=0;i<amount;i++)
                numbers[i]=temp[i];

		    
        }/*End of for loop */

        StartCoroutine(showsorted());
    }
    
    
    //
    //
    //INSERTION SORT
    //
    //

    public void insertionsorter()
    {
        StartCoroutine(insertionSort());
    }

    public IEnumerator insertionSort()
    {
        if (!sorting)
        {
            sorting = true;
            sort.text = "Sorting...";
            int i, key, j, n;
            n = amount;
            for (i = 1; i < n; i++) 
            {  
                key = numbers[i];  
                j = i - 1;  
                if (stop) yield break;
                /* Move elements of arr[0..i-1], that are  
                greater than key, to one position ahead  
                of their current position */
                while (j >= 0 && numbers[j] > key) 
                {  
                    varray.sqstore[j].GetComponent<SpriteRenderer>().color = Color.yellow;
                    varray.sqstore[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                    if(stop) yield break;
                    yield return new WaitForSeconds(1/((float)amount));
                    varray.sqstore[j].GetComponent<SpriteRenderer>().color = Color.red;
                    numbers[j + 1] = numbers[j];  
                    j = j - 1;  
                }  
                numbers[j + 1] = key;  
                varray.sqstore[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
    
            StartCoroutine(showsorted());
        }
    }

    
    //
    //
    //HEAP SORT
    //
    //

    public void heapsorter()
    {
        StartCoroutine(heapSort(amount));
    }
    
    void heapify(int n, int i) 
    {
            int largest = i; // Initialize largest as root 
            int l = 2*i + 1; // left = 2*i + 1 
            int r = 2*i + 2; // right = 2*i + 2 
      
            // If left child is larger than root 
            if (l < n && numbers[l] > numbers[largest]) 
                largest = l; 
      
            // If right child is larger than largest so far 
            if (r < n && numbers[r] > numbers[largest]) 
                largest = r; 
      
            // If largest is not root 
            if (largest != i)
            {
                
                varray.sqstore[largest].GetComponent<SpriteRenderer>().color = Color.yellow;
                int temp = numbers[i];
                numbers[i] = numbers[largest];
                numbers[largest] = temp; 
      
                // Recursively heapify the affected sub-tree 
                heapify(n, largest); 
            } 
        
    } 
  
// main function to do heap sort 
    public IEnumerator heapSort(int n) 
    {
        if (!sorting)
        {
            sorting = true;
            sort.text = "Sorting...";
            // Build heap (rearrange array) 
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                if (stop) yield break;
                varray.sqstore[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                yield return new WaitForSeconds(10/((float)amount));
                heapify(n, i);
                varray.sqstore[i].GetComponent<SpriteRenderer>().color = Color.red;
            } 
      
            // One by one extract an element from heap 
            for (int i=n-1; i>=0; i--) 
            { 
                if(stop) yield break;;
                // Move current root to end
                varray.sqstore[i].GetComponent<SpriteRenderer>().color = Color.blue;
                varray.sqstore[0].GetComponent<SpriteRenderer>().color = Color.blue;
                yield return new WaitForSeconds(10/((float)amount));
                varray.sqstore[0].GetComponent<SpriteRenderer>().color = Color.red;
                varray.sqstore[i].GetComponent<SpriteRenderer>().color = Color.white;
                int temp = numbers[0];
                numbers[0] = numbers[i];
                numbers[i] = temp;
      
                // call max heapify on the reduced heap 
                heapify(i, 0); 
            }

            StartCoroutine(showsorted());
        }
    }
    
    
    //
    //
    //SELECTION SORT
    //
    //

    public void selectionsorter()
    {
        StartCoroutine(selsort ());
    }

    public IEnumerator selsort()
    {
        if (!sorting)
        {
            sorting = true;
            sort.text = "Sorting...";
            int i, j, min_idx;  
      
            // One by one move boundary of unsorted subarray  
            for (i = 0; i < amount-1; i++)  
            {  
                varray.sqstore[i].GetComponent<SpriteRenderer>().color = Color.cyan;
                // Find the minimum element in unsorted array  
                min_idx = i;
                for (j = i + 1; j < amount; j++)
                {
                    varray.sqstore[j].GetComponent<SpriteRenderer>().color = Color.blue;
                    if (stop) yield break;
                    yield return new WaitForSeconds(1/((float)amount * 10f));
                    if (numbers[j] < numbers[min_idx])
                    {
                        min_idx = j;
                    }  
                    varray.sqstore[j].GetComponent<SpriteRenderer>().color = Color.red;
                    
                }
      
                // Swap the found minimum element with the first element  
                int tem = numbers[min_idx];
                numbers[min_idx] = numbers[i];
                numbers[i] = tem;
                varray.sqstore[i].GetComponent<SpriteRenderer>().color = Color.white;
            }

            StartCoroutine(showsorted());
        }
    }
}
