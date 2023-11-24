import { Fragment, useEffect, useState } from 'react';
import { Container} from 'semantic-ui-react';
import { Activity } from '../models/activitity';
import Navbar from './Navbar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import {v4 as uuid} from 'uuid';
import agent from '../api/agent';
import LoadingComponent from './LoadingComponent';
function App() {
  const [activities,setActivities]=useState<Activity[]>([]);
  const[selectedActivity,setSelectedactivity]=useState<Activity | undefined>(undefined);
  const[editMode,setEditMode]=useState(false);
  const[loading,setLoading]=useState(true);
  const[submiting,setSubmiting]=useState(false);

  useEffect(()=>{
    agent.Activities.list().then(response=>{
      let activities:Activity[]=[];
      response.forEach(activity=>{
        activity.date=activity.date.split('T')[0];
        activities.push(activity);
      })
      setActivities(activities)
      setLoading(false);
    })
  },[])

  function handleSelectActivity(id:string){
    setSelectedactivity(activities.find(x=>x.id==id));
  }

  function handleCancelSelectedActivtiy()
  {
    setSelectedactivity(undefined);
  }

  function handleFormOpen(id?:string){
    id?handleSelectActivity(id):handleCancelSelectedActivtiy();
    setEditMode(true);
  }

  function handleFormClose()
  {
    setEditMode(false);
  }
  function handleCreateOrEditActivity(activity:Activity)
  {
    setSubmiting(true);
    if(activity.id){
      agent.Activities.update(activity).then(()=>{
        setActivities([...activities.filter(x=>x.id!=activity.id),activity]);
        setSelectedactivity(activity);
        setEditMode(false);
        setSubmiting(false);
      });
    }else{
      activity.id=uuid();
      agent.Activities.create(activity).then(()=>{
        setActivities([...activities,{...activity,id:uuid()}]);  
        setSelectedactivity(activity);
        setEditMode(false);
        setSubmiting(false);
      });
    }
  }
  function handleDeleteActivity(id:string)
  {
    setSubmiting(true);
    agent.Activities.delete(id).then(()=>{
      setActivities([...activities.filter(x=>x.id!==id)]);
      setSubmiting(false);
    });
  }

  if(loading)return <LoadingComponent content='Loading App' />
  return (
    <Fragment>
      <Navbar openForm={handleFormOpen}/>
      <Container style={{marginTop:'7em'}}>
        <ActivityDashboard 
        activities={activities} 
        selectedActivity={selectedActivity}
        selectActivity={handleSelectActivity}
        cancelSelectActivity={handleCancelSelectedActivtiy}
        editMode={editMode}
        openForm={handleFormOpen}
        closeForm={handleFormClose}
        createOrEdit={handleCreateOrEditActivity}
        deleteActivity={handleDeleteActivity}
        submiting={submiting}
        />
      </Container>
    </Fragment>
  )
}

export default App
