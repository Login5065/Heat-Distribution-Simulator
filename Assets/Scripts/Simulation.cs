using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Punkt
{
    public double Value=0;
    public double SavedValue = 0;
    public bool exist = true;
    public int k;

}
public class Simulation
{
    public Punkt[,] siatka;
    public float alpha = 1;
    
    public (int x, int y) PointAmount;
    public float time_s;
    public float h;
    public float[] brzegi = new float[4]{0.0f,0.0f,0.0f,0.0f}; // 0=n 1=s 2=w 3=e;

  
    public void CreateBodiesVisible(GridGenerator GG1,GridGenerator GG2=null)
    {
        double temp2;
        int GY, GX;
        time_s = GG1.DeltaTime;
        alpha = (GG1.d*GG1.cp);
        
        
        if (GG2 == null)
        {
            temp2 = 0;
            GY = 0;
            GX = 0;
        }
        else
        {
            temp2 = GG2.Temp;
            GY = GG2.GridY;
            GX = GG2.GridX;
        }

        h = GG1.dist;
        int y1_extra=0, y2_extra=0;
        int X = GG1.GridX + GX +2 ;
        int Y;
        if (GG1.GridY > GY)
        {
            Y = GG1.GridY+2;
            y2_extra = (GG1.GridY - GY) / 2;
        }
        else 
        {
            Y = GY+2;
            y1_extra = (GY-GG1.GridY) / 2;
            
        }
        
        Debug.Log(y1_extra);
        Debug.Log(y2_extra);
        
        PointAmount = (X, Y);
        
        siatka = new Punkt[X, Y];
        for (int x = 0; x < X ; x++)
        {
            for (int y = 0; y < Y; y++)
            {
                siatka[x, y] = new Punkt();
                siatka[x, y].SavedValue =0;
                siatka[x, y].exist = false;
                siatka[x, y].k = 0;
            }
            
        }

        int a = 0;

        for (int i = 1; i <= GG1.GridX; i++)
        {
            
            for (int j = 1+y1_extra; j <= GG1.GridY+y1_extra; j++)
            {

                siatka[i, j].SavedValue =GG1.Temp;
                siatka[i, j].exist = true;
                GG1.Panel[a].punkt = siatka[i, j];
                siatka[i,j].k=GG1.k;
                a++;
            }
        }

        a = 0;
       
        if (GG2 != null)
        {
            for (int i = GG1.GridX+1 ; i < X - 1; i++)
            {

                for (int j = 1+y2_extra; j <= GY+y2_extra; j++)
                {
                    siatka[i, j].SavedValue = temp2;
                    siatka[i, j].exist = true;
                    GG2.Panel[a].punkt = siatka[i, j];
                    siatka[i,j].k=GG2.k;
                    a++;
                }
            }
        }

    }
    
    public void  Math()
    {

        for (int x = 0; x < PointAmount.x; x++)
        {
            for (int y = 0; y < PointAmount.y; y++)
            {
                siatka[x, y].Value =siatka[x,y].SavedValue;
                siatka[x, y].SavedValue = 0;
            }
            
        }
        
        ///liczenie bez brzegow
        for (int x = 1; x < PointAmount.x-1; x++)
        {
            for (int y = 1; y < PointAmount.y-1; y++)
            {
                if (siatka[x, y].exist)
                {
                    //siatka[x, y].SavedValue = T(siatka[x - 1, y], siatka[x + 1, y], siatka[x, y - 1], siatka[x, y + 1], h);
                    siatka[x, y].SavedValue = T_fix(siatka[x - 1, y], siatka[x + 1, y],siatka[x,y], siatka[x, y - 1], siatka[x, y + 1], h);
                 //   Debug.Log(siatka[x,y].SavedValue+" "+x + " "+y);
                }
            }
            //            Debug.Log("iteration");
        }
     

    }

    public double T(Punkt _x, Punkt x_, Punkt _y, Punkt y_, float h)
    { 
        //Debug.Log(_x.exist );
        //Debug.Log( x_.exist );
        //Debug.Log( y_.exist);
        //Debug.Log( _y.exist );

        double value = 0;
        Debug.Log(h);
        float H = h;
        int counter = 0;
        double _X=_x.Value, X_=x_.Value;
//        if (!_x.exist) _X = brzegi[2];
        if (!_x.exist) {_X = 0;
        }
        else
        {
            counter++;
        }

        value += _X ;
        Debug.Log(value);
// if (!x_.exist) X_ = brzegi[3];
        if (!x_.exist) {X_= 0;
        }
        else
        {
            counter++;
        }

        value += X_ ;
        
        Debug.Log(value);
        double _Y=_y.Value, Y_=y_.Value;
//        if (!_y.exist) _Y = brzegi[0];
        if (!_y.exist)
        {
            _Y = 0;
        }
        else
        {
            counter++;
        }

        value += _Y ;
        
        Debug.Log(value);
//        if (!y_.exist) Y_ = brzegi[1];
        if (!y_.exist)
        {
            Y_ = 0;
            
        }
        else
        {
            counter++;
        }

        value += Y_ ;
        
        Debug.Log(value);
        /*H = Mathf.Pow(h, 2);

        return (_X * H + X_ * H + _Y * H + Y_ * H) / (H * 4);
        */

        
        
        
        Debug.Log("dfjkdfhjdfhjdhfjdhfjdfhjdfhdj");
        
        
        return (value/counter) ;

    }


    public double T_fix(Punkt _x, Punkt x_,Punkt O, Punkt _y, Punkt y_, float h)
    {

        double max, min;
        double value = 0;
        float H = h*h;
        int counter = 0;
        double _X = _x.Value, X_ = x_.Value;
        if (!_x.exist) { _X = 0; }
        else { counter++; }


        if (!x_.exist) { X_ = 0; }
        else{ counter++; }
        
        
        /*if (_X > X_)
        {
            max = _X;
            min = X_;
        }else{
            max = X_;
            min = _X;
        }*/
        
        double _Y = _y.Value, Y_ = y_.Value;
        if (!_y.exist)
        { _Y = 0; }
        else
        { counter++; }

        if (!y_.exist)
        { Y_ = 0; }
        else
        { counter++; }

        


        value = _X*_x.k + X_*x_.k + _Y*_y.k + Y_*y_.k;

        double v2 = (_X + X_ + _Y + Y_) / counter; 
        
        value -= counter * O.Value*O.k;
        value /= alpha;
        value /= H;
        value *= time_s;
        value += O.Value;

        if (value > O.Value && v2 < value)
        {
            Debug.Log("Max interwal czasowy to " + H + " " + alpha + " " + time_s );
            value = v2;
        }else if (value < O.Value && v2 > value)
        {
            value = v2;
        }
        /*
        Debug.Log("val"+value);
        
        Debug.Log("valuelast");
        */


        


        return value;
    }

}
