using System.Collections.Generic;

enum Bean
{
    bean1,
    bean2
};

public class CoffeeBean
{
    Bean beanName;

    public int coffeecontent;

    public bool Check;
    public int CBean;

    public CoffeeBean(bool check, int coffeeBean)
    {
        Check = check;
        CBean = coffeeBean;
    }
}
