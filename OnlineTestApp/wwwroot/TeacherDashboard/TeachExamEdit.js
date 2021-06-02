
const app = Vue.createApp({
    data() {
        return {
            teacherUrl: "https://localhost:44308/api/Teachers/",
            examsUrl: "https://localhost:44308/api/Exams/",
            questionUrl: "https://localhost:44308/api/Question/",
            teacherName: "",
            userId: localStorage.currentUserId,
            userPassword: localStorage.password,
            examId: localStorage.currentExamId,
            examTitle: localStorage.currentExamTitle,
            firstQuestionToAdd:"",
            secondQuestionToAdd:"",
            thirdQuestionToAdd:"",
            fourthQuestionToAdd:"",
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
            questionArray: [
 
            ],
            toggleAddNewQuestion: false,

            newQuestionToSend:{
                question: "",
                choices: "",
                correct: "",
                points: 0,
                examId: 2010
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
                                console.log("new");
    
                                
    
    
                    
                            }) 
                        
                    }
        
                }) 

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
                    examId: this.newQuestionToSend.examId

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
        }


    },
    created(){
        this.init();

        


    },
    watch:{
        firstQuestion(){
            console.log(this.firstQuestion);
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
              <p class="lead">{{ question.question }}  {{ question.choices }}?</p>
          </div>
          <hr class="my-4">
          <div class="container">
          <div class="form-check">
              <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault1">
              <label class="form-check-label" for="flexRadioDefault1">
                {{ firstQuestion }}
              </label>
            </div>
  
            <div class="form-check">
              <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault1">
              <label class="form-check-label" for="flexRadioDefault1">
                {{ secondQuestion }}
              </label>
            </div>
  
            <div class="form-check">
              <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault1">
              <label class="form-check-label" for="flexRadioDefault1">
                {{ thirdQuestion }}
              </label>
            </div>
  
  
            <div class="form-check">
              <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault2" checked>
              <label class="form-check-label" for="flexRadioDefault2">
                {{ fourthQuestion }}
              </label>
            </div>
          </div>
            <hr>
            <div class="container">
            <div class="row">
                <div class="col-1 mb-4"><a class="btn btn-warning btn-lg shadow" v-on:click="editThisQuestion" href="#" role="button">edit</a></div>
                <div class="col-1 mb-4"><a class="btn btn-danger btn-lg shadow" href="#" role="button">delete</a></div>
                <div class="col-8"></div>
                <div class="col-2 pt-2"> <h4>Score: {{ question.points }}</h4> </div>
                
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
            fourthQuestion: ""



        }
    },
    methods:{
        editThisQuestion(){
            window.scrollTo(0, 0);
            this.$root.whichQuestionToEdit=this.indx;
            this.$root.questionIdToEdit= this.question.id;
            this.$root.editState = true;
            this.$root.addNewQuestionState= false;
            this.$root.questionToEdit = this.question;
            console.log(this.$root.questionToEdit);
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
                console.log(tempStr);
                
               
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
            console.log("before del");
            console.log(this.$root.questionArray); 
            this.$root.questionArray = [];  
            console.log("after del");
            console.log(this.$root.questionArray);  
            console.log("arrived to init in delete");
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
        console.log("created works"+this.question.question);
        this.unssembleTheAnswers();
    }

});

app.mount('#exam-edit-page');

