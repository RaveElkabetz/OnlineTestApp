const app = Vue.createApp({
    data() {
        return {
            studentUrl: "https://localhost:44308/api/Student/",
            examsUrl: "https://localhost:44308/api/Exams/",
            studentName: "",
            userId: localStorage.Id,
            userPassword: localStorage.password,
            examsArray: [],
            //toggleAddNewTest: false,


        };
    
    },
    methods: {
        logToConsole(){
            console.log("printed from the main app");
        }
        ,

        init(){
            fetch(this.studentUrl + this.userId).then((response) => {
                if (response.ok){
                        return response.json();
                    }
                })
                .then((data) =>{
                    console.log(data);
                    this.studentName = data.name;
                    if (data.password === this.userPassword) {
                        console.log("password match!");
                        //now need to show all his exams: GET all exam by teacher id-
                        fetch(this.examsUrl).then((response) => {
                            if (response.ok){
                                    return response.json();
                                }
                            })
                            .then((data) =>{
                                
                                
                                var examTemp={
                                    id: "",
                                    title: "",
                                    teacherId: "",
                                    dateOfTest: ""
                                }
                                var i = 0;
                                this.examsArray.push.apply(this.examsArray, data);
                                //this.examsArray.shift();
    
                                //console.log("new-logs-down");
                                //console.log(this.examsArray);
                                //console.log(data);
    
                                
    
    
                    
                            }) 
                        
                    }
        
                }) 
    

        },
        /*submitClickedNewTeacher(){
            this.newExamToSend.dateOfTest = this.newExamToSend.dateOfTest.slice(0,10);
            this.newExamToSend.dateOfTest += ("T" + this.newExamToSend.testHour+":00.000Z");
           
            fetch(this.examsUrl,{
                method: 'POST',
                headers:{
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify( {
                    title: this.newExamToSend.title,
                    teacherId: this.userId,
                    dateOfTest: this.newExamToSend.dateOfTest,
                    durationOfTest: this.newExamToSend.durationOfTest

                })

            });
            this.toggleAddNewTest= !this.toggleAddNewTest;
            this.examsArray = [];
             
            this.init();
            console.log("init after adding");
    

        },*/
  





    },
    mounted(){
        this.init();


    },
    watch:{
        examsArray(){
            if(this.examsArray[0]){
                this.toggleAddNewTest = true;
            }
        }
    }






});
app.component('exam-list-item',{
    props:['exam'],
    template:`<li class="d-flex justify-content-between shadow">
    <div class="d-flex flex-row align-items-center"><i class="fa fa-check-circle checkicon"></i>
        <div class="ml-2">
            <h6 class="mb-0">{{ exam.title }}</h6>
            <div class="d-flex flex-row mt-1 text-black-50 date-time">
                <div><i class="fa fa-calendar-o"></i><span class="ml-2"> {{printTheDate(exam.dateOfTest)}}  </span></div>
                
            </div>
        </div>
    </div>
    <div class="d-flex flex-row align-items-center">
        <div class="d-flex flex-column mr-2">
            <div class="profile-image">  <button v-on:click="enterToActiveExamWindow" type="button" class="btn btn-outline-secondary"><img class="" src="/icons/edit-2.png" width="35"></button> </div>
        </div> <i class="fa fa-ellipsis-h"></i>
    </div>
</li>`,
    data(){
        return{
            examDate: ""
            

        }
    },
    methods:{
        deleteThisTest(){
            console.log(this.exam);
            const id = this.exam.id;
            fetch('https://localhost:44308/api/Exams/' + id, {
                method: 'DELETE',
                })
                .then(res => res.text()) // or res.json()
                .then(res => console.log(res));
            console.log("before del");
            console.log(this.$root.examsArray); 
            this.$root.examsArray = [];  
            console.log("after del");
            console.log(this.$root.examsArray);  
            console.log("arrived to init in delete");
            this.$root.init();           
        },
        addMinutes(date, minutes) {
            return new Date(date.getTime() + minutes*60000);
        },
        dateInPast(firstDate, secondDate) {
            if (firstDate.setHours(0, 0, 0, 0) <= secondDate.setHours(0, 0, 0, 0)) {
              return true;
            }
          
            return false;
          },
        enterToActiveExamWindow(){
            console.log("strarted the func");
            var now = new Date();
            var testDatePlusTime = this.addMinutes(new Date(this.exam.dateOfTest),this.exam.durationMinutes);
            var theDateOfTest = this.exam.dateOfTest;
            var  theDurationOfTest = this.exam.durationMinutes ;
            console.log("before the if");
            if ( now > theDateOfTest &&  testDatePlusTime >= now) {
                localStorage.currentExamId = this.exam.id;
                localStorage.currentExamTitle = this.exam.title;
                localStorage.currentTeacherId = this.exam.teacherId;
                localStorage.currentStudentName = this.studentName;
                //the user id and password was sent ad Id And Password
                //
                console.log("in the if stat");
                window.location.href = 'https://localhost:44308/StudentDashboard/activeExam.html';
            }
            else if(testDatePlusTime < now )
            {
                window.alert("Sorry, can't participate in this test. the test is over.");

            }
            else{
                window.alert("This test has not started yet!!!");
            }

            

        },
        printTheDate(dateInput){
            //return dateInput;
            //console.log(localStorage.currentExamDate);
            //var myDate = localStorage.currentExamDate;
            var n = 3;
            if (dateInput.length === 20) {
                n=4;
                
            }
            else if (dateInput.length === 21) {
                n=5;
            }
            else if (dateInput.length === 18) {
                n=2;
            }

            else if (dateInput.length === 22) {
                n=6;
            }
            
            else if (dateInput.length === 23) {
                n=7;
            }
            else if (dateInput.length === 24) {
                n=8;
            }
            //2021-08-12T09:30:0

            var myDate = dateInput.substring(0, dateInput.length-n);
            
            const testDate = new Date(myDate).toDateString();
            
            //console.log("below is the date");
            //console.log(testDate);
            //var newDate = testDate.toString();
            //newDate= newDate.substring(0,21);
            //console.log(newDate);
            this.examDate = testDate;
            return this.examDate;
        }
    },
    beforeMount(){

    },
    mounted(){
        


    }
    


});
app.mount('#StudentDashboard');

//const Home = app.component('exam-list-item')

//console.log(localStorage);