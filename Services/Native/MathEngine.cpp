#include <cmath>
#include <algorithm>

extern "C"{
    __declspec(dllexport) double NavyMale(double waist , double neck , double height)
    {
					// Null kontrolü: C++'da double tipinde null yoktur, ancak mantýksal olarak 0 deðeri null gibi kabul edilebilir.
			if (waist == 0.0 || neck == 0.0 || height == 0.0)
						// Hatalý giriþ için özel bir deðer dönebiliriz, örneðin -1
				return -1.0;
					
					//BodyFat Percentage for Male
				double navyMale_Fat = 495/(1.0324 - 0.19077 * log10(waist-neck) + 0.15456 * log10(height))- 450;

				return navyMale_Fat;
    }
	__declspec(dllexport) double NavyFemale(double waist , double neck , double hip , double height)
	{
		if (waist == 0.0 || neck == 0.0 || hip == 0.0 || height == 0.0)
			return -1.0;
		
		//BodyFat Percentage for Female
		double  navyFemale_Fat =  495/ (1.29579 - 0.35004 * log10(waist + hip - neck) + 0.22100 * log10(height)) -450; 
	
		return navyFemale_Fat;
	}

	__declspec(dllexport) double JP3Male(double chest, double abdomen, double thigh, int age)
	{
		if (chest == 0.0 || abdomen == 0.0 || thigh == 0.0 || age == 0) 
			return -1.0;
		

		//Density calculation with 3 parameters 
		double Male_Density =  1.10938 - (0.0008267 * (chest + abdomen + thigh)) + (0.0000016 * pow((chest + abdomen + thigh),2)) -(0.0002574 * age);
		//BodyFat Percentage for Male
		double	Bodyfat_PM = (495/ Male_Density)-450;

		return Bodyfat_PM;
	}
	__declspec(dllexport) double JP3Female(double triceps ,double thigh, double suprailiac, int age )
	{   //Density calculation with 3 parameters 
		if (triceps == 0.0 || thigh == 0.0 || suprailiac == 0.0 || age == 0)
			return -1.0;

		double	Female_Density =  1.0994921 - (0.0009929 * (triceps + thigh + suprailiac)) + (0.0000023 * pow((triceps+thigh+suprailiac),2))-(0.0001392*age);
		//BodyFat Percentage for Female
		double 	BodyFat_PF = (495/Female_Density)-450;

		return BodyFat_PF;
	}


	__declspec(dllexport) double JP7Male(double sum7points,int age)
	{	//Density calculation with 3 parameters 
		if (sum7points == 0.0 || age == 0)
			return -1.0;


		double Male_Density = 1.112 -  (0.00043499 * sum7points) + (0.00000055* pow(sum7points,2))- (0.00028826 * age);
			//BodyFat Percentage for Male
		double Bodyfat_PM =  (495/Male_Density) - 450;
		return Bodyfat_PM;
	}
	__declspec(dllexport) double JP7Female(double sum7points,int age)
	{	//Density calculation with 3 parameters 
		if (sum7points == 0.0 || age == 0)
			return -1.0;

	    double Female_Density = 1.097 -  (0.00046971 * sum7points) + (0.00000056* pow(sum7points,2))- (0.00012828 * age);
			//BodyFat Percentage for Female
		double Bodyfat_PF =  (495/Female_Density) - 450;
		return Bodyfat_PF;

	}

	__declspec(dllexport)  double MifflinStJeorMale(double weight,double height , int age)
	{
		if (weight == 0.0 || height == 0.0 || age == 0.0)
			return -1.0;

		double bmr_Male = 10*weight + 6.25 * height - 5*age + 5;
		return bmr_Male;
	}
    __declspec(dllexport) double MifflinStJeorFemale(double weight , double height , int age)
	{
		if (weight == 0.0 || height == 0.0 || age == 0)
			return -1.0;

		double bmr_Female = 10*weight + 6.25 * height - 5 * age - 161;
		return bmr_Female;
	
	}
	__declspec(dllexport) double CunninghamFormula(double LeanBodyMass)
	{
		if (LeanBodyMass == 0.0)
			return -1.0;
		double Bmr_for_person = 370 + (21.6*LeanBodyMass);
		return Bmr_for_person;

	}
	__declspec(dllexport) double CalculateTDEE(double bmr, int activity_level)
	{
		if (bmr == 0.0 || activity_level == 0.0)
			return -1.0;

		switch(activity_level)
		{
			case 1: 
				bmr = bmr*1.2;
				return bmr;
				break;
			case 2:
				bmr = bmr*1.375;
				return bmr;
				break;
			case 3:
				bmr = bmr*1.55;
				return bmr;
				break;
			case 4:
				bmr = bmr*1.725;
				return bmr;
				break;
			case 5: 
				bmr = bmr*1.9;
				return bmr;
				break;
			default: return bmr * 1.2;
		}
	}
}
