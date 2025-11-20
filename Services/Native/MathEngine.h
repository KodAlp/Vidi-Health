#ifndef  MATHENGINE_H
#define  MATHENGINE_H


#ifdef  _WIN32
	#define DLL_EXPORT __declspec(dllexport)
#else
	#define DLL_EXPORT 
#endif

extern "C"
{
    DLL_EXPORT double NavyMale(double waist, double neck, double height);
    DLL_EXPORT double NavyFemale(double waist, double neck, double hip, double height);

    // Jackson-Pollock 3-Point Functions
    DLL_EXPORT double JP3Male(double chest, double abdomen, double thigh, int age);
    DLL_EXPORT double JP3Female(double triceps, double abdomen, double suprailiac, int age);

    // Jackson-Pollock 7-Point Functions
    DLL_EXPORT double JP7Male(double sum7points, int age);
    DLL_EXPORT double JP7Female(double sum7points, int age);

    // BMR Calculation Functions
    DLL_EXPORT double MifflinStJeorMale(double weight, double height, int age);
    DLL_EXPORT double MifflinStJeorFemale(double weight, double height, int age);
    DLL_EXPORT double CunninghamFormula(double LeanBodyMass);

    // TDEE Calculation Function
    DLL_EXPORT double CalculateTDEE(double bmr, int activity_level);

}

#endif