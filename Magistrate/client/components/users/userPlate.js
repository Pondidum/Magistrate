import React from 'react'
import Plate from '../plate'

const UserPlate = ({ content }) => (
  <Plate title={content.name}>
    <span>Roles: {content.roles.length}</span>
    <span>Includes: {content.includes.length}</span>
    <span>Revokes: {content.revokes.length}</span>
  </Plate>
)

export default UserPlate
