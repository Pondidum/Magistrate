import React from 'react'

const FilterBar = ({ filterChanged }) => (
  <div className="filter-bar col-sm-5 pull-right">
    <input
      type="text"
      className="form-control"
      placeholder="filter..."
      onChange={e => filterChanged(e.target.value)}
    />
  </div>
);

export default FilterBar
