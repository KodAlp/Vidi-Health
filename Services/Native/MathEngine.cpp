#include <cmath>
#include <algorithm>

extern "C"{
	__declspec(dllexport) double NavyMale(double waist , double neck , double height);
	__declspec(dllexport) double NavyFemale(double waist , double neck , double hip , double height);
	__declspec(dllexport) double JP3Male(double chest, double abdomen, double thigh, int age);
	__declspec(dllexport) double JP3Female(double triceps ,double thigh, double suprailiac, int age );
	__declspec(dllexport) double JP7Male(double sum7points,int age , bool isMale);
	__declspec(dllexport) double JP7Female(double sum7points,intage,bool isMale)
	__declspec(dllexport) double ApplyEthnicCorrection(double bodyFat, int ethnicity);


}
