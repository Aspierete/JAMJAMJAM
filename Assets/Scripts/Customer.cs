using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [SerializeField] GameObject[] allShipParts;
    [SerializeField] List<ShipPart> customerOrder;
    [SerializeField] List<Image> orderIconList;
    [SerializeField] GameObject iconContainer;
    [SerializeField] Image orderIcon;

    private int orderAmount;



    void Start()
    {
        CustomerOrder();
        ShowOrderIcons();
    }

    private void CustomerOrder()
    {
        orderAmount = Random.Range(1, 3);

        for (int i = 0; i < orderAmount; i++)
        {
            int randomShipPartIndex = Random.Range(0, allShipParts.Length);
            customerOrder.Add(allShipParts[randomShipPartIndex].GetComponent<ShipPart>());


            // UI STUFF. change it later. 

        }
    }

    public void CheckDelivery(ShipPart deliveredPart)
    {
        foreach (ShipPart shipPart in customerOrder)
        {
            if (deliveredPart.shipParts == shipPart.shipParts)
            {
                print("true order");
                customerOrder.Remove(shipPart);

                OrderIconUpdate();
                CompleteOrder();
                return;
            }
            else
            {
                print("wrong order");
                Destroy(gameObject);
            }
        }
    }

    private void ShowOrderIcons()
    {
        for (int i = 0; i < customerOrder.Count; i++)
        {
            orderIconList.Add(Instantiate(orderIcon, iconContainer.transform));
            orderIconList[i].sprite = customerOrder[i].shipPartIcon;
        }
    }

    private void CompleteOrder()
    {
        if (customerOrder.Count == 0)
        {
            // earn money 
            GameManager.Instance.EarnMoney(orderAmount);
            Destroy(gameObject);
        }
    }


    // This is a very cheesy solution for updating customer order UI when a part delivered.
    // However I couldn't find a better solution atm. 
    // It might need a refactor later. 
    private void OrderIconUpdate()
    {
        Destroy(orderIconList[0]);
        orderIconList.RemoveAt(0);

        for (int i = 0; i < orderIconList.Count; i++)
        {
            orderIconList[i].sprite = customerOrder[i].shipPartIcon;
        }
    }
}
