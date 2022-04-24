import React from 'react'
import './location-info.css'
import { IssLocation } from '../../api/types'
import { timestampToDate } from '../ustils/date-utils'

interface Props {
    location: IssLocation
}

export const LocationInfo = ({location}: Props) => {
    
    return (
        <div className='location-info-container'>
            <span>Time: { timestampToDate(location.timestamp) }</span>
            <span>Latitude: {location.position.latitude}</span>
            <span>Longitude: {location.position.longitude}</span>
        </div>
    )
}