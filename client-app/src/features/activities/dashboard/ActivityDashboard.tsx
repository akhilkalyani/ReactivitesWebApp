import { Grid } from 'semantic-ui-react';
import ActivityList from './ActivityList';
import { useStore } from '../../../app/stores/store';
import { observer } from 'mobx-react-lite';
import { useEffect } from 'react';
import LoadingComponent from '../../../app/layout/LoadingComponent';
import ActivityFilter from './ActivityFilter';

// eslint-disable-next-line react-refresh/only-export-components
export default observer(function ActivityDashboard(){

    const {activityStore}=useStore();
    const {loadActivities,activityRegistry}=activityStore;
    useEffect(()=>{
        if(activityRegistry.size==0)
            loadActivities();
    },[activityRegistry.size, loadActivities])
  
    if(activityStore.loadingInitial)return <LoadingComponent content='Loading Activities' />
    
    return(
        <Grid>
            <Grid.Column width='10'>
                <ActivityList />
            </Grid.Column>
            <Grid.Column width='6'>
                <ActivityFilter />   
            </Grid.Column>
        </Grid>
    )
})