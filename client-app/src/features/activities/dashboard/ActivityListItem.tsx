import { Button, Icon, Item, ItemImage, Label, Segment } from "semantic-ui-react";
import { Activity } from "../../../app/models/activitity";
import { Link } from "react-router-dom";
import { useStore } from "../../../app/stores/store";
import { SyntheticEvent, useState } from "react";

interface Prop{
    activity:Activity;
}

export default function ActivityListItem({activity}:Prop){
    const {activityStore}=useStore();
    const {deleteActivity,loading}=activityStore;
    const [target,setTarget]=useState('');
    function handleDeleteActivity(e:SyntheticEvent<HTMLButtonElement>,id:string)
    {
        setTarget(e.currentTarget.name);
        deleteActivity(id);
    }
    return(
        <Segment.Group>
            <Segment>
                <Item.Group>
                    <Item>
                        <ItemImage size='tiny' circular src='/assets/user.png' />
                        <Item.Content>
                            <Item.Header as={Link} to={`/activities/${activity.id}`}>
                                {activity.title}
                            </Item.Header>
                        <Item.Description>Hosted bu Bob</Item.Description>
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Segment>
            <Segment>
                <span>
                    <Icon name='clock' />{activity.date}
                    <Icon name='marker' />{activity.venue}
                </span>
            </Segment>
            <Segment secondary>
                Attendies go here
            </Segment>
            <Segment clearing>
                <span>{activity.description}</span>
                <Button 
                as={Link}
                to={`/activities/${activity.id}`}
                color='teal'
                floated='right'
                content='View'
                />
            </Segment>
        </Segment.Group>
    )
} 