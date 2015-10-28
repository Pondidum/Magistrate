var FilterBar = React.createClass({

  onChange(e) {
    this.props.filterChanged(e.target.value);
  },

  render() {

    return (
      <div className="row" style={{ marginBottom: "1em" }}>
        <div className="pull-right col-md-5">
          <input type="text" className="form-control" placeholder="filter..." onChange={this.onChange} ref="filter" />
        </div>
      </div>
    );
  }

});
