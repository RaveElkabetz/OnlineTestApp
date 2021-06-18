const app = Vue.createApp({
    data() {
        return {
            studentUrl: "https://localhost:44308/api/Student/",
            examsUrl: "https://localhost:44308/api/Exams/",
            studentName: "",
            userId: localStorage.Id,
            userPassword: localStorage.password,
            examsArray: [],
            tooltipTriggerList: "",
            tooltipList: ""


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
          <div class="row">
            <div class="col"><h5 style="color: green; margin-top: 10px;">Start:</h5></div>
            <div class="col"><div class="profile-image">  <button v-on:click="enterToActiveExamWindow" type="button" class="btn btn-outline-success" ><img class="" src="/icons/edit-2.png" width="35"></button></div>
  
          </div>
        
             </div>
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
  
        enterToActiveExamWindow(){
            
            var  theDurationOfTest = this.exam.durationMinutes ;
            var testDatePlusTime = this.addMinutes(new Date(this.exam.dateOfTest),theDurationOfTest);
            var theDateOfTest = moment(new Date(this.exam.dateOfTest));
            var testTimePlusDuration = moment(testDatePlusTime);
            var nowDate = moment(new Date());
            
            
            
            if ( nowDate.isBefore(testDatePlusTime) && nowDate.isAfter(theDateOfTest) ) {
                localStorage.currentExamId = this.exam.id;
                localStorage.currentExamTitle = this.exam.title;
                localStorage.currentTeacherId = this.exam.teacherId;
                localStorage.currentStudentName = this.studentName;
                //the user id and password was sent ad Id And Password
                //
                
                window.location.href = 'https://localhost:44308/StudentDashboard/activeExam.html';
            }
            else if(nowDate.isAfter(testDatePlusTime))   
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