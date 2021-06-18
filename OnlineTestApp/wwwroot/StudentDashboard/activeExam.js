
const app = Vue.createApp({
    data() {
        return {
            teacherUrl: "https://localhost:44308/api/Teachers/",
            studentUrl: "https://localhost:44308/api/Student",
            examsUrl: "https://localhost:44308/api/Exams/",
            questionUrl: "https://localhost:44308/api/Question/",
            examInstancesUrl: "https://localhost:44308/api/ExamInstance/",
            examInstancesUrlByExamId: "https://localhost:44308/api/ExamInstance/GetByExamId/",
            currentExamInstanceScore : 0,
            teacherName: "",
            userId: localStorage.currentUserId,
            userPassword: localStorage.password,
            examId: localStorage.currentExamId,
            examTitle: localStorage.currentExamTitle,
            examDate: localStorage.currentExamDate,
            editExamMode: true,
            showGradesMode: false,
            isTheCurrentExmaHasStarted: false,
            countDownClock: "test",
            //switchModes: null,
            firstQuestionToAdd:"",
            secondQuestionToAdd:"",
            thirdQuestionToAdd:"",
            fourthQuestionToAdd:"",
            theCorrectAnswer:"",
            //-----
            firstAnswerToEdit:"",
            secondAnswerToEdit:"",
            thirdAnswerToEdit:"",
            fourthAnswerToEdit:"",
            //----
            
            questionIdToEdit: -1,
            whichQuestionToEdit: -1,
            editState : false,
            addNewQuestionState: true,
            examsInstancesArray:[],
            questionArray: [
 
            ],
            toggleAddNewQuestion: false,

            newQuestionToSend:{
                question: "",
                choices: "",
                correct: "",
                points: 0,
                examId: 0
            },

            questionToEdit:{
                question: "",
                choices: "",
                correct: "",
                points: 0,
                examId: 0
            }

        };
    
    },
    methods: {
        init(){
            //formating the date->
            


            console.log(localStorage);
            this.examId = localStorage.currentExamId;
            fetch(this.teacherUrl + this.userId).then((response) => {
                if (response.ok){
                        return response.json();
                    }
                })
                .then((data) =>{
                    console.log(data);
                    this.teacherName = data.name;
                    if (data.password === this.userPassword) {
                        console.log("password match!");
                        //now need to show all his exams: GET all exam by teacher id-
                        console.log("trying to fetch exam num "+this.examId+" "+this.examTitle);
                        fetch(this.questionUrl + this.examId).then((response) => {
                            if (response.ok){
                                    return response.json();
                                }
                            })
                            .then((data) =>{
                                this.questionArray.push.apply(this.questionArray, data);
                                //this.examsArray.shift();
                                
                                console.log("question array");
                                console.log(this.questionArray);
                                console.log("question array from data");
                                console.log(data);

    
                                
    
    
                    
                            }) 
                        
                    }
        
                }) 

  

        },
        startOrContinueTheCountdown()
        {
            this.countDownClock = moment(new Date(this.examDate)).countdown().toString();
        },
        submitEditThisQuestion(){
            //this.clearEditOrNewFields();
 
        },
        dateInPast(firstDate, secondDate) {
            if (firstDate.setHours(0, 0, 0, 0) <= secondDate.setHours(0, 0, 0, 0)) {
              return true;
            }
          
            return false;
          },
        switchBackToGradesMode(){
            
            this.editExamMode=false;
            this.showGradesMode=true;

 //+=================================================
            console.log(localStorage.currentExamDate);
            var myDate = localStorage.currentExamDate;
            var n = 3;
            if (myDate.length === 20) {
                n=4;
                
            }
            else if (myDate.length === 21) {
                n=5;
            }
            else if (myDate.length === 18) {
                n=2;
            }

            else if (myDate.length === 22) {
                n=6;
            }
            
            else if (myDate.length === 23) {
                n=7;
            }
            else if (myDate.length === 24) {
                n=8;
            }
            //2021-08-12T09:30:0

            myDate = myDate.substring(0, myDate.length-n);
            
            const testDate = new Date(myDate);
            console.log("below is the date");
            console.log(testDate);
            var newDate = testDate.toString();
            newDate= newDate.substring(0,21);
            console.log(newDate);
            this.examDate = newDate;


            var today = new Date();
            this.isTheCurrentExmaHasStarted = this.dateInPast(testDate,today);
            if (this.isTheCurrentExmaHasStarted) {

                
            }






console.log(this.examInstancesUrlByExamId);
console.log(this.examId);
            fetch(this.teacherUrl + this.userId).then((response) => {
                
                if (response.ok){
                        return response.json();
                    }
                })
                .then((data) =>{
                    console.log(data);
                    this.teacherName = data.name;
                    if (data.password === this.userPassword) {
                        console.log("password match!");
                        //now need to show all his exams: GET all exam by teacher id-
                        fetch(this.examInstancesUrlByExamId + this.examId).then((response) => {// להמשיך מכאן לחבר קומפוננטה אל הציוניפ
                            if (response.ok){
                                    return response.json();
                                }
                            })
                            .then((data) =>{
                                
                                
                                
                           
                                
                             var i = 0;
                             
                             
                             
                             //this.examsArray.shift();
                        
                             console.log("new-logs-down");
                             console.log(this.examsInstancesArray);
                             console.log(data);

                
                            
                            this.examsInstancesArray = [];
                            this.examsInstancesArray.push.apply(this.examsInstancesArray, data);

                    
                            }) 
                        
                    }
        
                }
                
                
                ) 


            
        },
        switchBackToExamMode(){
            this.examsInstancesArray = [];

 


//================================================
            this.showGradesMode=false;
            this.editExamMode = true;
            
        },
        switchModes(){
            
            
            if (this.editExamMode && !this.showGradesMode) {
                this.switchBackToGradesMode();
                return;
            }
            if(!this.editExamMode && this.showGradesMode){
                
                this.switchBackToExamMode();
                return;
            }
        },
        switchToAddNewQuestionState(){
            console.log("switched to add new mode");
            this.clearEditOrNewFields();
            this.editState = false;
            this.addNewQuestionState = true;
        },
        clearEditOrNewFields(){
            this.firstQuestionToAdd = "";
            this.secondQuestionToAdd = "";
            this.thirdQuestionToAdd = "";
            this.fourthQuestionToAdd = "";
            this.theCorrectAnswer = "";

            this.newQuestionToSend.score = 0;
            this.newQuestionToSend.question="";
            



        },
        submitNewQuestion()
        {
            if(this.editState){
                //fetch UPDATE to server
                //return;
            }
            console.log("entered to submitNewQuestion");
            this.assembleTheAnswers();
           
            fetch(this.questionUrl,{
                method: 'POST',
                headers:{
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify( {
                    question: this.newQuestionToSend.question,
                    choices: this.newQuestionToSend.choices,
                    correct: this.newQuestionToSend.correct,
                    points: this.newQuestionToSend.points,
                    examId: this.examId

                })

            });
            //this.toggleAddNewTest= !this.toggleAddNewTest;
            this.$root.questionArray = [];
             
            this.$root.init();
    

        }
        ,

        assembleTheAnswers(){
            this.newQuestionToSend.choices="";
            this.newQuestionToSend.choices+=this.firstQuestionToAdd+ ';' +this.secondQuestionToAdd+';'+this.thirdQuestionToAdd+';'+this.fourthQuestionToAdd;
        },
        


    },
    created(){
        this.init();

        


    },
    watch:{
        firstQuestion(){
            
        }
    
    }






});


app.component('question-list-item',{
    props:['question','indx'],
    template:`
    <li >
    <div >
    <div class="card question-card shadow" style="width: 55rem;">
      <div class=" ">
          <div class="container">
              <h1 class="display-4">Question {{ indx+1 }}</h1>
              <p class="lead">{{ question.question }}</p>
          </div>
          <hr class="my-4">
          <div class="container">
          <div class="form-check">
              <input class="form-check-input" type="radio" @change="onChange($event)" value="1" :name="indx" id="flexRadioDefault1">
              <label class="form-check-label" for="flexRadioDefault1">
                {{ firstQuestion }}
              </label>
            </div>
  
            <div class="form-check">
              <input class="form-check-input" type="radio" @change="onChange($event)" value="2" :name="indx" id="flexRadioDefault2">
              <label class="form-check-label" for="flexRadioDefault2">
                {{ secondQuestion }}
              </label>
            </div>
  
            <div class="form-check">
              <input class="form-check-input" type="radio" @change="onChange($event)" value="3" :name="indx" id="flexRadioDefault3">
              <label class="form-check-label" for="flexRadioDefault3">
                {{ thirdQuestion }}
              </label>
            </div>
  
  
            <div class="form-check">
              <input class="form-check-input" type="radio" @change="onChange($event)" value="4" :name="indx" id="flexRadioDefault4" >
              <label class="form-check-label" for="flexRadioDefault4">
                {{ fourthQuestion }}
              </label>
            </div>
          </div>
            <hr>
            <div class="container">
            <div class="row">
               
                <div class="col-2 pt-2"> <h4 style="padding-bottom: 20px;">Score: {{ question.points }}</h4> </div>
                <div class="col-10"></div>
               
                
            </div>
          </div>
          
        </div>
  </div>
  </div>
  </li>
  <p>&ensp;</p>`,
    data(){
        return{
            firstQuestion: "",
            secondQuestion: "",
            thirdQuestion: "",
            fourthQuestion: "",
            answerChosen: ""



        }
    },
    methods:{
        onChange(event){
            console.log("changed!");
            this.answerChosen = event.target.value;
            console.log(this.answerChosen);
            this.checkThisQuestion();
        },
        
        checkThisQuestion(){
            console.log(parseInt(this.question.correct));
            console.log(this.answerChosen);
            if(parseInt(this.question.correct) == this.answerChosen){
                console.log("entered if");
                this.$root.currentExamInstanceScore+= this.question.points;
                console.log("below this is the totla score of the test");
                console.log(this.$root.currentExamInstanceScore);
            }








        },

        editThisQuestion(){
            window.scrollTo(0, 0);
            this.$root.whichQuestionToEdit=this.indx;
            this.$root.questionIdToEdit= this.question.id;
            this.$root.editState = true;
            this.$root.addNewQuestionState= false;
            this.$root.questionToEdit = this.question;
            
            this.$root.newQuestionToSend.question = this.question.question;
            this.$root.firstQuestionToAdd = this.firstQuestion;
            this.$root.secondQuestionToAdd = this.secondQuestion;
            this.$root.thirdQuestionToAdd = this.thirdQuestion;
            this.$root.fourthQuestionToAdd = this.fourthQuestion;
            this.$root.newQuestionToSend.score = this.points;


        },
        unssembleTheAnswers(){
            var pos = 0;
            var t = 0;
            var tempStr = this.question.choices;
            for (let i = 0; i < 4; i++)
            {
                //  "dfd;sfs;ret;gbgb"
                pos =tempStr.indexOf(";");
                if(i===0) this.firstQuestion = tempStr.slice(0,pos);
                if(i===1) this.secondQuestion = tempStr.slice(0,pos);
                if(i===2) this.thirdQuestion = tempStr.slice(0,pos);
                if(i===3) this.fourthQuestion = tempStr.slice(0,tempStr.length);
                tempStr = tempStr.slice(pos+1,tempStr.length)
                
                
               
                //t=pos+2;                


            }

        },
        deleteThisQuestion(){
            console.log(this.questionObj);
            const id = this.questionObj.id;
            fetch(questionUrl + id, {
                method: 'DELETE',
                })
                .then(res => res.text()) // or res.json()
                .then(res => console.log(res));
             
            this.$root.questionArray = [];  

            this.$root.init();           
        },
        enterToEditExamWindow(){
            localStorage.currentExamId = this.exam.id;
            localStorage.currentExamTitle = this.exam.title;
            localStorage.currentUserId = this.exam.teacherId;
            
            window.location.href = 'https://localhost:44308/TeacherDashboard/TeachExamEdit.html';
            

        }
    },
    mounted(){
       // console.log("created works"+this.question.question);
        this.unssembleTheAnswers();
    }

});
app.component('exam-grade-list-item',{
    props:['exam'],
    template:`<li class="grades d-flex justify-content-between  shadow" style="
        background-color: antiquewhite;
        border-radius: 4px;
        border-color:  rgb(148, 124, 105);
        border-style: initial;
        margin-bottom: 1%;"
        >
    <div class="d-flex flex-row align-items-center"><i class="fa fa-check-circle checkicon"></i>
        <div class="ml-2">
            <h6 class="mb-0">Student Id: {{ exam.studentId }}</h6>
            <div class="d-flex flex-row mt-1 text-black-50 date-time">
                <div><i class="fa fa-calendar-o"></i><span class="ml-2"> {{exam.dateOfTest}}  </span></div>
                
            </div>

        </div>
    </div>
    <div class="d-flex flex-row align-items-center">
        <div class="d-flex flex-column mr-2">
            <div class="profile-image">             
            <h5>
            Grade: {{ exam.grade }}
            </h5> </div>
        </div> <i class="fa fa-ellipsis-h"></i>
    </div>
</li>
`,
    data(){
        return{

        }
    },
    methods:{
        deleteThisTest(){/*
            console.log(this.exam);
            const id = this.exam.teacherId;
            fetch('https://localhost:44308/api/ExamInstance/' + id, {
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
            this.$root.init();   */        
        },
        enterToEditExamWindow(){
            /*
            localStorage.currentExamId = this.exam.id;
            localStorage.currentExamTitle = this.exam.title;
            localStorage.currentUserId = this.exam.teacherId;
            localStorage.currentExamDate = this.exam.dateOfTest;
            localStorage.currentExam = this.exam;
            
            window.location.href = 'https://localhost:44308/TeacherDashboard/TeachExamEdit.html';
            */
            

        }
    }

});

app.mount('#exam-edit-page');
console.log(app);

