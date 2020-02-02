export const template = `<div id="grid-requests"></div>
                        <div id="grid-workers"></div>
                        <div id="settings-form-container">
                          <form>
                            <div>
                               <label>
                                 T<sub>m</sub>
                                 <input class="k-textbox" id="timeManager" type="number" name="timeManager" min="10" max="100"  />
                               </label>
                            </div>
                            <div>
                              <label>
                                 T<sub>d</sub>
                                 <input class="k-textbox" id="timeDirector" type="number" name="timeDirector" min="10" max="100"  />
                              </label>
                            </div>
                            <div>
                              <label>
                                 LIM<sub>l</sub>
                                 <input class="k-textbox" id="executeTimeLimitLeft" type="number" name="executeTimeLimitLeft" min="10" max="100"  />
                              </label>
                            </div>
                            <div>
                              <label>
                                 LIM<sub>r</sub>
                                 <input class="k-textbox" id="executeTimeLimitRight" type="number" name="executeTimeLimitRight" min="10" max="100"  />
                              </label>
                            </div>
                            <div style="width: 100%">
                               <button class="k-button" type="button">Сохранить</button>
                            </div>
                          </from>
                        </div>`;