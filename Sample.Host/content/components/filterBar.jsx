var FilterBar = React.createClass({

  onChange(e) {
    this.props.filterChanged(e.target.value);
  },

  render() {

    return (
      <div className="col-sm-5 pull-right">
        <input type="text" className="form-control" placeholder="filter..." onChange={this.onChange} ref="filter" />
      </div>
    );
  }

});
