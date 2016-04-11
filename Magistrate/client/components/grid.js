import React from 'react'

const Grid = (props) => {

  const { collection, filter } = props;

  var tiles = collection
    .filter((item, index) => filter(item, index))
    .map((item, i) => (
      <props.tile key={i} content={item} />
    ));

  return (
    <div className="row">
      <ul className="list-unstyled list-inline col-sm-12">
        {tiles}
      </ul>
    </div>
  )
}

export default Grid
