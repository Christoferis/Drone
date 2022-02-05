//just a random struct for having 3 values at once


struct double3
{
    /* data */
    double x;
    double y;
    double z;

    double3& operator+(double3 adder){
        x += adder.x;
        y += adder.y;
        z += adder.z;

        return *this;
    }

    double3& operator-(double3 adder){
        x -= adder.x;
        y -= adder.y;
        z -= adder.z;

        return *this;
    }
};


